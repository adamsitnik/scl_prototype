using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;
using System.CommandLine.Symbols;
using System.Threading;
using System.Threading.Tasks;

// DESIGN: this type is expected to be used frequently
namespace System.CommandLine;

public abstract class SymbolRouter
{
    private protected readonly Dictionary<CliSymbol, Func<ParseResult, int>> _syncMap = new();
    private protected CliCommand? _rootCommand;

    private protected SymbolRouter(CliCommand? rootCommand) => _rootCommand = rootCommand;

    // DESIGN: it's not returning a reference to this because it's not a builder
    public void Use(CliSymbol symbol, Func<ParseResult, int> action)
        => _syncMap.Add(
            symbol ?? throw new ArgumentNullException(nameof(symbol)),
            action ?? throw new ArgumentNullException(nameof(action)));

    // DESIGN: not every user wants to return an exit code in explicit way, this overload just returns 0
    public void Use(CliSymbol symbol, Action<ParseResult> action)
        => _syncMap.Add(
            symbol ?? throw new ArgumentNullException(nameof(symbol)),
            parseResult => { action.Invoke(parseResult); return 0; });

    public CliCommand AddCommand<T1, T2>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, Func<T1?, T2?, int> action)
    {
        CliCommand command = new(commandName);
        Add(symbol1, command);
        Add(symbol2, command);

        // TODO: this requires more work, as it's not intuitive
        if (_rootCommand is null)
        {
            _rootCommand = command;
        }
        else
        {
            _rootCommand.Subcommands.Add(command);
        }

        _syncMap.Add(command, parseResult =>
        {
            T1? value1 = GetValue(parseResult, symbol1);
            T2? value2 = GetValue(parseResult, symbol2);

            return action.Invoke(value1, value2);
        });

        return command;
    }

    public CliCommand AddCommand<T1, T2>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, Action<T1?, T2?> action)
        => AddCommand(commandName, symbol1, symbol2, (value1, value2) => { action(value1, value2); return 0; });

    private protected static int RunSynchronous(ParseResult parseResult, Func<ParseResult, int> action)
    {
        try
        {
            action.Invoke(parseResult);
            return 0;
        }
        catch (Exception)
        {
            // TODO: log error
            return -1;
        }
    }

    private protected static void Add<T>(IParsableSymbol<T> symbol, CliCommand command)
    {
        if (symbol is CliOption<T> option)
        {
            command.Options.Add(option);
        }
        else if (symbol is CliArgument<T> argument)
        {
            command.Arguments.Add(argument);
        }
    }

    private static T? GetValue<T>(ParseResult parseResult, IParsableSymbol<T> symbol)
    {
        if (symbol is CliOption<T> option)
        {
            return parseResult.GetValue<T>(option);
        }
        else if (symbol is CliArgument<T> argument)
        {
            return parseResult.GetValue<T>(argument);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
}

// DESIGN: sync and async methods kept separately ON PURPOSE to avoid sync over async
public sealed class SynchronousSymbolRouter : SymbolRouter
{
    public SynchronousSymbolRouter(CliCommand? rootCommand = default) : base(rootCommand)
    {
    }

    public int Run(string[] args) => Run(_rootCommand!.Parse(args));

    public int Run(ParseResult parseResult)
    {
        if (parseResult.Errors.Count > 0)
        {
            // TODO should we print errors during parsing or here?
            return -1;
        }

        CliSymbol terminatingSymbol = parseResult.TerminatingSymbolResult switch
        { 
            CommandResult commandResult => commandResult.Command,
            OptionResult optionResult => optionResult.Option,
            ArgumentResult argumentResult => argumentResult.Argument,
            DirectiveResult directiveResult => directiveResult.Directive,
            _ => throw new NotSupportedException()
        };

        if (!_syncMap.TryGetValue(terminatingSymbol, out var action))
        {
            // TODO: print an error
            return -1;
        }

        return RunSynchronous(parseResult, action);
    }
}

public sealed class AsynchronousSymbolRouter : SymbolRouter
{
    private readonly Dictionary<CliSymbol, Func<ParseResult, CancellationToken, Task<int>>> _asyncMap = new();

    public AsynchronousSymbolRouter(CliCommand? rootCommand = default) : base(rootCommand)
    {
    }

    public void Use(CliSymbol symbol, Func<ParseResult, CancellationToken, Task<int>> action)
        => _asyncMap.Add(
            symbol ?? throw new ArgumentNullException(nameof(symbol)),
            action ?? throw new ArgumentNullException(nameof(action)));

    public void Use(CliSymbol symbol, Func<ParseResult, CancellationToken, Task> action)
        => _asyncMap.Add(
            symbol ?? throw new ArgumentNullException(nameof(symbol)),
            async (parseResult, cancellationToken) =>
            {
                await action.Invoke(parseResult, cancellationToken);
                return 0;
            });

    public async Task<int> RunAsync(ParseResult parseResult, CancellationToken cancellationToken = default)
    {
        if (parseResult.Errors.Count > 0)
        {
            // TODO should we print errors during parsing or here?
            return -1;
        }

        // DESIGN: this type allows to run both sync and async actions, as we expect that
        // most apps will start with adding sync actions and eventually might decide
        // later to use async for just a single command.
        // In such case, the users will need to simply change the type,
        // without the need to use different Use overloads
        if (_syncMap.TryGetValue(parseResult.CommandResult.Command, out var action))
        {
            return RunSynchronous(parseResult, action);
        }
        else if (_asyncMap.TryGetValue(parseResult.CommandResult.Command, out var asyncAction))
        {
            try
            {
                await asyncAction.Invoke(parseResult, cancellationToken);
            }
            catch (Exception)
            {
                // TODO: log error
                return -1; ;
            }
        }

        // TODO: print an error
        return -1;
    }
}
