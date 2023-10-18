namespace System.CommandLine.Symbols;

/// <summary>
/// The purpose of directives is to provide cross-cutting functionality that can apply across command-line apps.
/// Because directives are syntactically distinct from the app's own syntax, they can provide functionality that applies across apps.
/// 
/// A directive must conform to the following syntax rules:
/// * It's a token on the command line that comes after the app's name but before any subcommands or options.
/// * It's enclosed in square brackets.
/// * It doesn't contain spaces.
/// </summary>
public class CliDirective : CliSymbol
{
    /// <summary>
    /// Initializes a new instance of the Directive class.
    /// </summary>
    /// <param name="name">The name of the directive. It can't contain whitespaces.</param>
    public CliDirective(string name) : base(name) { }
}
