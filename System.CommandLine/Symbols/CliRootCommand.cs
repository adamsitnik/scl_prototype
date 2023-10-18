using System.Collections.Generic;
using System.CommandLine.Help;
using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used, that it's why in the main namespace
namespace System.CommandLine;

public class CliRootCommand : CliCommand
{
    public CliRootCommand(string name, string? description = null) : base(name, description)
    {
        Options.Add(new HelpOption());
        Options.Add(new VersionOption());
    }

    /// <summary>
    /// Represents all of the directives for the command.
    /// </summary>
    public IList<CliDirective> Directives { get; } = new List<CliDirective>();

    public override void Add(CliSymbol symbol)
    {
        if (symbol is CliDirective directive) Directives.Add(directive);
        else base.Add(symbol);
    }

    public override IEnumerable<CliSymbol> Children
    {
        get
        {
            foreach (CliSymbol child in base.Children) yield return child;
            foreach (CliDirective directive in Directives) yield return directive;
        }
    }
}
