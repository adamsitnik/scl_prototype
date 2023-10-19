using System.CommandLine.Symbols;

namespace System.CommandLine.Parsing;

/// <summary>
/// A unit of significant text on the command line.
/// </summary>
public sealed class CliToken : IEquatable<CliToken>
{
    /// <param name="value">The string value of the token.</param>
    /// <param name="type">The type of the token.</param>
    /// <param name="symbol">The symbol represented by the token</param>
    public CliToken(string? value, TokenType type, CliSymbol symbol)
    {
        Value = value ?? "";
        Type = type;
        Symbol = symbol;
    }

    /// <summary>
    /// The string value of the token.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// The type of the token.
    /// </summary>
    public TokenType Type { get; }

    internal CliSymbol Symbol { get; }

    public bool Equals(CliToken? other) => Value.Equals(other?.Value) && Type.Equals(other.Type);
}
