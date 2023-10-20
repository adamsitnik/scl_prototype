using System.CommandLine.Symbols;

namespace System.CommandLine.Parsing;

/// <summary>
/// A result produced when parsing an <see cref="CliArgument"/>.
/// </summary>
public sealed class ArgumentResult : SymbolResult
{
    internal ArgumentResult(CliArgument argument, SymbolResult? parent) : base(parent) => Argument = argument;

    /// <summary>
    /// The argument to which the result applies.
    /// </summary>
    public CliArgument Argument { get; }

    // DESIGN: GetValueOrDefault got removed, it's not available during parsing
}
