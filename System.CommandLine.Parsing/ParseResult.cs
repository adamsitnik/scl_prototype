using System.Collections.Generic;
using System.CommandLine.Symbols;
using System.Linq;

namespace System.CommandLine.Parsing;

/// <summary>
/// Describes the results of parsing a command line input based on a specific parser configuration.
/// </summary>
public sealed class ParseResult
{
    internal ParseResult(CliConfiguration configuration, CommandResult rootCommandResult, CommandResult commandResult,
        List<CliToken> tokens, IReadOnlyList<CliToken>? unmatchedTokens, List<ParseError>? errors, string? commandLineText = null)
    {
        Configuration = configuration;
        RootCommandResult = rootCommandResult;
        CommandResult = commandResult;
        Errors = errors ?? (IReadOnlyList<ParseError>)Array.Empty<ParseError>();
        Tokens = tokens;
        UnmatchedTokens = unmatchedTokens is null ? Array.Empty<string>() : unmatchedTokens.Select(token => token.Value).ToArray();
    }

    /// <summary>
    /// A result indicating the command specified in the command line input.
    /// </summary>
    public CommandResult CommandResult { get; }

    /// <summary>
    /// The configuration used to produce the parse result.
    /// </summary>
    public CliConfiguration Configuration { get; }

    /// <summary>
    /// Gets the root command result.
    /// </summary>
    public CommandResult RootCommandResult { get; }

    /// <summary>
    /// Gets the parse errors found while parsing command line input.
    /// </summary>
    public IReadOnlyList<ParseError> Errors { get; }

    /// <summary>
    /// Gets the tokens identified while parsing command line input.
    /// </summary>
    public IReadOnlyList<CliToken> Tokens { get; }

    /// <summary>
    /// Gets the list of tokens used on the command line that were not matched by the parser.
    /// </summary>
    public string[] UnmatchedTokens { get; }

    /// <summary>
    /// Gets the parsed or default value for the specified argument.
    /// </summary>
    /// <param name="argument">The argument for which to get a value.</param>
    /// <returns>The parsed value or a configured default.</returns>
    public T? GetValue<T>(CliArgument<T> argument) => throw new NotImplementedException();

    /// <summary>
    /// Gets the parsed or default value for the specified option.
    /// </summary>
    /// <param name="option">The option for which to get a value.</param>
    /// <returns>The parsed value or a configured default.</returns>
    public T? GetValue<T>(CliOption<T> option) => throw new NotImplementedException();

    /// <summary>
    /// Gets the parsed or default value for the specified symbol name, in the context of parsed command (not entire symbol tree).
    /// </summary>
    /// <param name="name">The name of the Symbol for which to get a value.</param>
    /// <returns>The parsed value or a configured default.</returns>
    /// <exception cref="InvalidOperationException">Thrown when parsing resulted in parse error(s).</exception>
    /// <exception cref="ArgumentException">Thrown when there was no symbol defined for given name for the parsed command.</exception>
    /// <exception cref="InvalidCastException">Thrown when parsed result can not be casted to <typeparamref name="T"/>.</exception>
    public T? GetValue<T>(string name) => throw new NotImplementedException();

    /// <summary>
    /// Gets the result, if any, for the specified argument.
    /// </summary>
    /// <param name="argument">The argument for which to find a result.</param>
    /// <returns>A result for the specified argument, or <see langword="null"/> if it was not provided and no default was configured.</returns>
    public ArgumentResult? FindResultFor(CliArgument argument) => throw new NotImplementedException();
    public OptionResult? FindResultFor(CliOption option) => throw new NotImplementedException();
    public CommandResult? FindResultFor(CliCommand command) => throw new NotImplementedException();
    public DirectiveResult? FindResultFor(CliDirective directive) => throw new NotImplementedException();
    public SymbolResult? FindResultFor(CliSymbol symbol) => throw new NotImplementedException();
}
