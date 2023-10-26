using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.CommandLine.Symbols;
using System.Threading;
using System.Threading.Tasks;

// DESIGN: most users will probably use CommandLineApplicationBuilder type
namespace System.CommandLine.Routing;

// DESIGN: sync and async methods are not kept separately, this can lead to sync over async but is simpler than doubling work everywhere
// TODO: better name
public sealed class SymbolRouter
{
    private readonly Dictionary<CliSymbol, Func<ParseResult, int>> _syncMap = new();
    private readonly Dictionary<CliSymbol, Func<ParseResult, CancellationToken, Task<int>>> _asyncMap = new();

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

    // DESIGN: should this method have Async suffix? Rather not as it does not need to be awaited?!
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

    // DESIGN: two different names rather than overload so the compiler always knows which type to assign to var
    public bool TryGetSynchronousAction(SymbolResult symbolResult, out Func<ParseResult, int>? action)
    {
        CliSymbol terminatingSymbol = symbolResult switch
        {
            CommandResult commandResult => commandResult.Command,
            OptionResult optionResult => optionResult.Option,
            ArgumentResult argumentResult => argumentResult.Argument,
            DirectiveResult directiveResult => directiveResult.Directive,
            _ => throw new NotSupportedException()
        };

        return _syncMap.TryGetValue(terminatingSymbol, out action);
    }

    public bool TryGetAsynchronousAction(SymbolResult symbolResult, out Func<ParseResult, CancellationToken, Task<int>>? action)
    {
        CliSymbol terminatingSymbol = symbolResult switch
        {
            CommandResult commandResult => commandResult.Command,
            OptionResult optionResult => optionResult.Option,
            ArgumentResult argumentResult => argumentResult.Argument,
            DirectiveResult directiveResult => directiveResult.Directive,
            _ => throw new NotSupportedException()
        };

        return _asyncMap.TryGetValue(terminatingSymbol, out action);
    }
}