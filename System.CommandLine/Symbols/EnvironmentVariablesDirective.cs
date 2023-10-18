// DESIGN: this type will be referenced by CliRootCommand and enabled by default, that is why it's not in the main namespace
namespace System.CommandLine.Symbols;

/// <summary>
/// Enables the use of the <c>[env:key=value]</c> directive, allowing environment variables to be set from the command line during invocation.
/// </summary>
public sealed class EnvironmentVariablesDirective : CliDirective
{
    public EnvironmentVariablesDirective() : base("env") { }
}
