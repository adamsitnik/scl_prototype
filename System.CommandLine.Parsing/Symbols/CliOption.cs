using System.Collections.Generic;

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

    /// <summary>
    /// Gets or sets the <see cref="Type" /> that the argument token(s) will be converted to.
    /// </summary>
    public abstract Type ValueType { get; }
}
