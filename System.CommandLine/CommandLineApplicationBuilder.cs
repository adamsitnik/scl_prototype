using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;
using System.CommandLine.Routing;

namespace System.CommandLine
{
    public class CommandLineApplicationBuilder
    {
        private readonly SymbolRouter _map = new();

        public CliCommand RootCommand { get; }

        public CommandLineApplicationBuilder(CliCommand? rootCommand = default) => RootCommand = rootCommand ?? new CliRootCommand();

        public CommandLineApplication Build()
        {
            // TODO: add actions defined by CliRootCommand
            return new(RootCommand, _map);
        }

        // DESIGN: parent could be 1st or 2nd argument and made mandatory
        /// <param name="parent">When it's not provided, <see cref="RootCommand"/> is used as parent of the new Command.</param>
        public CliCommand AddCommand<T1, T2>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, Func<T1?, T2?, int> action, CliCommand? parent = default)
        {
            CliCommand command = new(commandName);
            Add(symbol1, command);
            Add(symbol2, command);

            (parent ?? RootCommand).Subcommands.Add(command);

            _map.Use(command, parseResult =>
            {
                T1? value1 = GetValue(parseResult, symbol1);
                T2? value2 = GetValue(parseResult, symbol2);

                return action.Invoke(value1, value2);
            });

            return command;
        }

        public CliCommand AddCommand<T1, T2>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, Action<T1?, T2?> action)
            => AddCommand(commandName, symbol1, symbol2, (value1, value2) => { action(value1, value2); return 0; });

        public CliCommand AddCommand<T1, T2, T3>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, Func<T1?, T2?, T3?, int> action, CliCommand? parent = default)
        {
            CliCommand command = new(commandName);
            Add(symbol1, command);
            Add(symbol2, command);
            Add(symbol3, command);

            (parent ?? RootCommand).Subcommands.Add(command);

            _map.Use(command, parseResult =>
            {
                T1? value1 = GetValue(parseResult, symbol1);
                T2? value2 = GetValue(parseResult, symbol2);
                T3? value3 = GetValue(parseResult, symbol3);

                return action.Invoke(value1, value2, value3);
            });

            return command;
        }

        public CliCommand AddCommand<T1, T2, T3>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, Action<T1?, T2?, T3?> action, CliCommand? parent = default)
            => AddCommand(commandName, symbol1, symbol2, symbol3, (value1, value2, value3) => { action(value1, value2, value3); return 0; });

        public CliCommand AddCommand<T1, T2, T3, T4>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, IParsableSymbol<T4> symbol4, Func<T1?, T2?, T3?, T4?, int> action, CliCommand? parent = default)
        {
            CliCommand command = new(commandName);
            Add(symbol1, command);
            Add(symbol2, command);
            Add(symbol3, command);
            Add(symbol4, command);

            (parent ?? RootCommand).Subcommands.Add(command);

            _map.Use(command, parseResult =>
            {
                T1? value1 = GetValue(parseResult, symbol1);
                T2? value2 = GetValue(parseResult, symbol2);
                T3? value3 = GetValue(parseResult, symbol3);
                T4? value4 = GetValue(parseResult, symbol4);

                return action.Invoke(value1, value2, value3, value4);
            });

            return command;
        }

        public CliCommand AddCommand<T1, T2, T3, T4>(string commandName, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, IParsableSymbol<T4> symbol4, Action<T1?, T2?, T3?, T4?> action, CliCommand? parent = default)
            => AddCommand(commandName, symbol1, symbol2, symbol3, symbol4, (value1, value2, value3, value4) => { action(value1, value2, value3, value4); return 0; });

        public CliCommand AddCommand<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string commandName,
            IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, IParsableSymbol<T4> symbol4,
            IParsableSymbol<T5> symbol5, IParsableSymbol<T6> symbol6, IParsableSymbol<T7> symbol7, IParsableSymbol<T8> symbol8, IParsableSymbol<T9> symbol9,
            Func<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?, int> action, CliCommand? parent = default)
        {
            CliCommand command = new(commandName);
            Add(symbol1, command);
            Add(symbol2, command);
            Add(symbol3, command);
            Add(symbol4, command);
            Add(symbol5, command);
            Add(symbol6, command);
            Add(symbol7, command);
            Add(symbol8, command);
            Add(symbol9, command);

            (parent ?? RootCommand).Subcommands.Add(command);

            _map.Use(command, parseResult =>
            {
                T1? value1 = GetValue(parseResult, symbol1);
                T2? value2 = GetValue(parseResult, symbol2);
                T3? value3 = GetValue(parseResult, symbol3);
                T4? value4 = GetValue(parseResult, symbol4);
                T5? value5 = GetValue(parseResult, symbol5);
                T6? value6 = GetValue(parseResult, symbol6);
                T7? value7 = GetValue(parseResult, symbol7);
                T8? value8 = GetValue(parseResult, symbol8);
                T9? value9 = GetValue(parseResult, symbol9);

                return action.Invoke(value1, value2, value3, value4, value5, value6, value7, value8, value9);
            });

            return command;
        }

        public CliCommand AddCommand<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string commandName,
            IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, IParsableSymbol<T4> symbol4,
            IParsableSymbol<T5> symbol5, IParsableSymbol<T6> symbol6, IParsableSymbol<T7> symbol7, IParsableSymbol<T8> symbol8, IParsableSymbol<T9> symbol9,
            Action<T1?, T2?, T3?, T4?, T5?, T6?, T7?, T8?, T9?> action, CliCommand? parent = default)
                => AddCommand(commandName, symbol1, symbol2, symbol3, symbol4, symbol5, symbol6, symbol7, symbol8, symbol9, 
                    (value1, value2, value3, value4, value5, value6, value7, value8, value9) => { action(value1, value2, value3, value4, value5, value6, value7, value8, value9); return 0; });

        private static void Add<T>(IParsableSymbol<T> symbol, CliCommand command)
        {
            if (symbol is CliOption<T> option)
            {
                command.Options.Add(option);
            }
            else if (symbol is CliArgument<T> argument)
            {
                command.Arguments.Add(argument);
            }
            else
            {
                throw new InvalidOperationException();
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
}
