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

    /// <summary>
    /// Specifies the maximum number of tokens to consume for the argument. Remaining tokens are passed on and can be consumed by later arguments, or will otherwise be added to <see cref="ParseResult.UnmatchedTokens"/>
    /// </summary>
    /// <param name="numberOfTokens">The number of tokens to take. The rest are passed on.</param>
    /// <exception cref="ArgumentOutOfRangeException">numberOfTokens - Value must be at least 1.</exception>
    /// <exception cref="InvalidOperationException">Thrown if this method is called more than once.</exception>
    /// <exception cref="NotSupportedException">Thrown if this method is called by Option-owned ArgumentResult.</exception>
    public void OnlyTake(int numberOfTokens) { }

    internal static Func<ArgumentResult, T> GetDefaultParser<T>()
    {
        throw new NotImplementedException();
        //if (typeof(T) == typeof(int))
        //{
        //    return argumentResult => (T)(object)int.Parse(argumentResult.Tokens[^1].Value);
        //}
    }
}
