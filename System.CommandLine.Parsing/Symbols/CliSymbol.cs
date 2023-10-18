using System.Collections.Generic;

// DESIGN: not included in System.CommandLine namespace since most of the users won't ever need to use CliSymbol type
namespace System.CommandLine.Symbols;

public abstract class CliSymbol
{
    // DESIGN: we don't allow for custom symbols by design. The users can derive only from CliCommand, CliArgument<T>, CliOption<T> and Directive.
    private protected CliSymbol(string name) => Name = name;

    // DESIGN: it's virtual so the users can customize the behavior. Example: lazily load the description from resources
    /// <summary>
    /// Gets or sets the description of the symbol.
    /// </summary>
    public virtual string? Description { get; set; }

    /// <summary>
    /// Indicates that the symbol terminates a command line parsing.
    /// Example: help.
    /// </summary>
    // DESIGN: simple things should be simple, advanced possible. Setting it to true requires creating a custom derived type.
    public bool Terminating { get; protected set; } = false;

    /// <summary>
    /// Gets the name of the symbol.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the symbol is hidden.
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    /// Gets the parent symbols.
    /// </summary>
    public IEnumerable<CliSymbol> Parents => throw new NotImplementedException();
}
