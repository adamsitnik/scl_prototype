using System.Collections.Generic;
using System.CommandLine.Symbols;

// DESIGN: it's not in the main namespace, as I expect few users to need to provide custom parsers
namespace System.CommandLine.Parsing;

// DESIGN: So far, both Argument and Option were exposing a custom parser that was accepting ArgumentResult (not an OptionResult for Option).
// This type was exposing AddError method, which was callable during the parsing (desired) and after the parsing is finished (BAD).
// This type was created to allow for parsing both Argument and Option, without using Symbol-specific name
// and to allow for adding errors only during parsing.
// It's a struct as I expect that it will get created for every parse operation.
// TODO: it's missing the Parent property. Is that acceptable?
public readonly struct ParseInput
{
    internal ParseInput(CliSymbol symbol) => Symbol = symbol;

    public CliSymbol Symbol { get; }

    /// <summary>
    /// The list of tokens associated with this symbol result during parsing.
    /// </summary>
    public IReadOnlyList<CliToken> Tokens => throw new NotImplementedException();

    /// <summary>
    /// Adds an error message for this symbol result to it's parse tree.
    /// </summary>
    /// <remarks>Setting an error will cause the parser to indicate an error for the user and prevent invocation of the command line.</remarks>
    public void AddError(string errorMessage) { }

    /// <summary>
    /// Specifies the maximum number of tokens to consume for the symbol. Remaining tokens are passed on and can be consumed by later arguments, or will otherwise be added to <see cref="ParseResult.UnmatchedTokens"/>
    /// </summary>
    /// <param name="numberOfTokens">The number of tokens to take. The rest are passed on.</param>
    /// <exception cref="ArgumentOutOfRangeException">numberOfTokens - Value must be at least 1.</exception>
    /// <exception cref="InvalidOperationException">Thrown if this method is called more than once.</exception>
    /// <exception cref="NotSupportedException">Thrown if this method is called by Option-owned ArgumentResult.</exception>
    public void OnlyTake(int numberOfTokens) { }

    internal static Func<ParseInput, T> GetDefaultParser<T>()
    {
        throw new NotImplementedException();
        //if (typeof(T) == typeof(int))
        //{
        //    return argumentResult => (T)(object)int.Parse(argumentResult.Tokens[^1].Value);
        //}
    }
}
