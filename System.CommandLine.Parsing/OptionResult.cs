using System.CommandLine.Symbols;

namespace System.CommandLine.Parsing;

/// <summary>
/// A result produced when parsing an <see cref="CliOption" />.
/// </summary>
public sealed class OptionResult : SymbolResult
{
    internal OptionResult(CliOption option, CliToken? token = null, CommandResult? parent = null) : base(parent)
    {
        Option = option;
        IdentifierToken = token;
    }

    /// <summary>
    /// The option to which the result applies.
    /// </summary>
    public CliOption Option { get; }

    /// <summary>
    /// Indicates whether the result was created implicitly and not due to the option being specified on the command line.
    /// </summary>
    /// <remarks>Implicit results commonly result from options having a default value.</remarks>
    public bool Implicit { get; }

    /// <summary>
    /// The token that was parsed to specify the option.
    /// </summary>
    public CliToken? IdentifierToken { get; }

    // DESIGN: GetValueOrDefault got removed, it's not available during parsing
}