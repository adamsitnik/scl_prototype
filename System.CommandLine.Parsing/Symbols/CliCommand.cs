using System.Collections;
using System.Collections.Generic;
using System.CommandLine.Symbols;
using System.ComponentModel;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

/// <summary>
/// Represents a specific action that the application performs.
/// </summary>
/// <remarks>
/// Use the Command object for actions that correspond to a specific string (the command name). See
/// <see cref="RootCommand"/> for simple applications that only have one action. For example, <c>dotnet run</c>
/// uses <c>run</c> as the command.
/// </remarks>
public class CliCommand : CliSymbol, IEnumerable<CliSymbol>
{
    /// <summary>
    /// Initializes a new instance of the Command class.
    /// </summary>
    /// <param name="name">The name of the command.</param>
    /// <param name="description">The description of the command, shown in help.</param>
    public CliCommand(string name, string? description = null) : base(name) => Description = description;

    /// <summary>
    /// Gets the child symbols.
    /// </summary>
    // DESIGN: it's virtual so CliRootCommand can add Directives
    public virtual IEnumerable<CliSymbol> Children
    {
        get
        {
            foreach (var command in Subcommands)
                yield return command;

            foreach (var option in Options)
                yield return option;

            foreach (var argument in Arguments)
                yield return argument;
        }
    }

    /// <summary>
    /// Represents all of the arguments for the command.
    /// </summary>
    public IList<CliArgument> Arguments { get; } = new List<CliArgument>();

    /// <summary>
    /// Represents all of the options for the command, including global options that have been applied to any of the command's ancestors.
    /// </summary>
    public IList<CliOption> Options { get; } = new List<CliOption>();

    /// <summary>
    /// Represents all of the subcommands for the command.
    /// </summary>
    public IList<CliCommand> Subcommands { get; } = new List<CliCommand>();

    /// <summary>
    /// Gets the unique set of strings that can be used on the command line to specify the command.
    /// </summary>
    /// <remarks>The collection does not contain the <see cref="CliSymbol.Name"/> of the Command.</remarks>
    public ICollection<string> Aliases { get; } = new HashSet<string>();

    /// <summary>
    /// Adds a <see cref="CliSymbol"/> to the command.
    /// </summary>
    /// <param name="symbol">The symbol to add to the command.</param>
    [EditorBrowsable(EditorBrowsableState.Never)] // hide from intellisense, it's public for C# duck typing
    // DESIGN: it's virtual so CliRootCommand can add Directives
    public virtual void Add(CliSymbol symbol)
    {
        switch (symbol)
        { 
            case CliOption option: Options.Add(option); break;
            case CliArgument argument: Arguments.Add(argument); break;
            case CliCommand command: Subcommands.Add(command); break;
            default: throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Represents all of the symbols for the command.
    /// </summary>
    public IEnumerator<CliSymbol> GetEnumerator() => Children.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
