// DESIGN: this type will be referenced by CliRootCommand and enabled by default, that is why it's not in the main namespace
namespace System.CommandLine.Symbols;

/// <summary>
/// Enables the use of the <c>[diagram]</c> directive, which when specified on the command line will short 
/// circuit normal command handling and display a diagram explaining the parse result for the command line input.
/// </summary>
/// Example: dotnet [diagram] build -c Release -f net7.0
/// Output: [ dotnet [ build [ -c <Release> ] [ -f <net7.0> ] ] ]
public sealed class DiagramDirective : CliDirective
{
    /// <param name="errorExitCode">If the parse result contains errors, this exit code will be used when the process exits.</param>
    public DiagramDirective() : base("diagram") { }

    public int ParseErrorReturnValue { get; set; } = 1;
}
