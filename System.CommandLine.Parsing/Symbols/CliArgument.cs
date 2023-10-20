// DESIGN: not included in System.CommandLine namespace since most of the users won't ever need to use CliArgument type
using System.CommandLine.Parsing;

namespace System.CommandLine.Symbols;

/// <summary>
/// A symbol defining a value that can be passed on the command line to a <see cref="CliCommand">command</see> or <see cref="CliOption">option</see>.
/// </summary>
public abstract class CliArgument : CliSymbol
{
    private protected CliArgument(string name) : base(name) { }

    // DESIGN: HelpName is not included, as Help was moved out of main package

    /// <summary>
    /// Gets or sets the arity of the argument.
    /// </summary>
    public ArgumentArity Arity { get; set; }

    /// <summary>
    /// Gets the <see cref="Type" /> that the argument token(s) will be converted to.
    /// </summary>
    public abstract Type ValueType { get; }

    public abstract bool TryGetDefaultValue(out object? defaultValue);

#if NET7_0_OR_GREATER
    // DESIGN: these methods should allow us to avoid referencing code that tries to create parser for any T
    public static CliArgument<T> CreateParsable<T>(string name)
        where T : IParsable<T>
    {
        return new CliArgument<T>(name, static parseInput => T.Parse(parseInput.Tokens[^1].Value, provider: null));
    }

    public static CliArgument<T[]> CreateParsableArray<T>(string name)
        where T : IParsable<T>
    {
        return new CliArgument<T[]>(name, static parseInput =>
        {
            T[] parsed = new T[parseInput.Tokens.Count];

            for (int i = 0; i < parseInput.Tokens.Count; i++)
            {
                parsed[i] = T.Parse(parseInput.Tokens[i].Value, provider: null);
            }

            return parsed;
        });
    }
#endif
}
