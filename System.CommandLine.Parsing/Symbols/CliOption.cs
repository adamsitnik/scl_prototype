using System.Collections.Generic;
using System.CommandLine.Parsing;

// DESIGN: not included in System.CommandLine namespace since most of the users won't ever need to use CliOption type
namespace System.CommandLine.Symbols;

/// <summary>
/// A symbol defining a named parameter and a value for that parameter. 
/// </summary>
public abstract class CliOption : CliSymbol
{
    private protected CliOption(string name) : base(name) { }

    // DESIGN: HelpName is not included, as Help was moved out of main package

    /// <summary>
    /// When set to true, this option will be applied to the command and recursively to subcommands.
    /// It will not apply to parent commands.
    /// </summary>
    public bool Recursive { get; set; }

    /// <summary>
    /// Indicates whether the option is required when its parent command is invoked.
    /// </summary>
    /// <remarks>When an option is required and its parent command is invoked without it, an error results.</remarks>
    public bool Required { get; set; }

    /// <summary>
    /// Gets the unique set of strings that can be used on the command line to specify the Option.
    /// </summary>
    /// <remarks>The collection does not contain the <see cref="CliSymbol.Name"/> of the Option.</remarks>
    public ICollection<string> Aliases { get; } = new HashSet<string>();

    public ArgumentArity Arity { get; set; }

    /// <summary>
    /// Gets a value that indicates whether multiple argument tokens are allowed for each option identifier token.
    /// </summary>
    /// <example>
    /// If set to <see langword="true"/>, the following command line is valid for passing multiple arguments:
    /// <code>
    /// > --opt 1 2 3
    /// </code>
    /// The following is equivalent and is always valid:
    /// <code>
    /// > --opt 1 --opt 2 --opt 3
    /// </code>
    /// </example>
    public bool AllowMultipleArgumentsPerToken { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Type" /> that the argument token(s) will be converted to.
    /// </summary>
    public abstract Type ValueType { get; }

    public abstract bool TryGetDefaultValue(out object? defaultValue);

#if NET7_0_OR_GREATER
    // DESIGN: these methods should allow us to avoid referencing code that tries to create parser for any T
    public static CliOption<T> CreateParsable<T>(string name, params string[] aliases)
        where T : IParsable<T>
    {
        return new CliOption<T>(name, static parseInput => T.Parse(parseInput.Tokens[^1].Value, provider: null), aliases);
    }

    public static CliOption<T[]> CreateParsableArray<T>(string name, params string[] aliases)
        where T : IParsable<T>
    {
        return new CliOption<T[]>(name, static parseInput =>
        {
            T[] parsed = new T[parseInput.Tokens.Count];

            for (int i = 0; i < parseInput.Tokens.Count; i++)
            {
                parsed[i] = T.Parse(parseInput.Tokens[i].Value, provider: null);
            }
            
            return parsed;
        }, aliases);
    }
#endif
}
