using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;

// DESIGN: this API must be easy to discover, hence it's in the main namespace
namespace System.CommandLine;

/// <summary>
/// Parses command line input.
/// </summary>
public static class CliParser
{
    /// <summary>
    /// Parses a list of arguments.
    /// </summary>
    /// <param name="command">The command to use to parse the command line input.</param>
    /// <param name="args">The string array typically passed to a program's <c>Main</c> method.</param>
    /// <param name="configuration">The configuration on which the parser's grammar and behaviors are based.</param>
    /// <returns>A <see cref="ParseResult"/> providing details about the parse operation.</returns>
    public static ParseResult Parse(CliCommand command, IReadOnlyList<string> args, CliConfiguration? configuration = null) => throw new NotImplementedException();

    /// <summary>
    /// Parses a command line string.
    /// </summary>
    /// <param name="command">The command to use to parse the command line input.</param>
    /// <param name="commandLine">The complete command line input prior to splitting and tokenization. This input is not typically available when the parser is called from <c>Program.Main</c>. It is primarily used when calculating completions via the <c>dotnet-suggest</c> tool.</param>
    /// <param name="configuration">The configuration on which the parser's grammar and behaviors are based.</param>
    /// <remarks>The command line string input will be split into tokens as if it had been passed on the command line.</remarks>
    /// <returns>A <see cref="ParseResult"/> providing details about the parse operation.</returns>
    public static ParseResult Parse(CliCommand command, string commandLine, CliConfiguration? configuration = null) => Parse(command, SplitCommandLine(commandLine), configuration);

    /// <summary>
    /// Splits a string into a sequence of strings based on whitespace and quotation marks.
    /// </summary>
    /// <param name="commandLine">A command line input string.</param>
    /// <returns>A sequence of strings.</returns>
    public static string[] SplitCommandLine(string commandLine) => throw new NotImplementedException();

    public static (T1?, T2?) Parse<T1, T2>(string commandName, string[] args, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2)
    {
        CliCommand command = new(commandName);

        Add(symbol1, command);
        Add(symbol2, command);

        ParseResult parseResult = command.Parse(args);

        return (GetValue(parseResult, symbol1), GetValue(parseResult, symbol2));
    }

    public static (T1?, T2?, T3?) Parse<T1, T2, T3>(string commandName, string[] args, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3)
    {
        CliCommand command = new(commandName);

        Add(symbol1, command);
        Add(symbol2, command);
        Add(symbol3, command);

        ParseResult parseResult = command.Parse(args);

        return (GetValue(parseResult, symbol1), GetValue(parseResult, symbol2), GetValue(parseResult, symbol3));
    }

    public static (T1?, T2?, T3?, T4?) Parse<T1, T2, T3, T4>(string commandName, string[] args, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, IParsableSymbol<T4> symbol4)
    {
        CliCommand command = new(commandName);

        Add(symbol1, command);
        Add(symbol2, command);
        Add(symbol3, command);
        Add(symbol4, command);

        ParseResult parseResult = command.Parse(args);

        return (GetValue(parseResult, symbol1), GetValue(parseResult, symbol2), GetValue(parseResult, symbol3), GetValue(parseResult, symbol4));
    }

    public static (T1?, T2?, T3?, T4?, T5?) Parse<T1, T2, T3, T4, T5>(string commandName, string[] args, IParsableSymbol<T1> symbol1, IParsableSymbol<T2> symbol2, IParsableSymbol<T3> symbol3, IParsableSymbol<T4> symbol4, IParsableSymbol<T5> symbol5)
    {
        CliCommand command = new(commandName);

        Add(symbol1, command);
        Add(symbol2, command);
        Add(symbol3, command);
        Add(symbol4, command);
        Add(symbol5, command);

        ParseResult parseResult = command.Parse(args);

        return (GetValue(parseResult, symbol1), GetValue(parseResult, symbol2), GetValue(parseResult, symbol3), GetValue(parseResult, symbol4), GetValue(parseResult, symbol5));
    }

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
