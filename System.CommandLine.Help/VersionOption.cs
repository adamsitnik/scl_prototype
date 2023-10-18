namespace System.CommandLine.Help;

public sealed class VersionOption : CliOption<bool>
{
    /// <summary>
    /// When added to a <see cref="CliCommand"/>, it enables the use of a <c>--version</c> option, which when specified in command line input will short circuit normal command handling and instead write out version information before exiting.
    /// </summary>
    public VersionOption() : base("--version", Array.Empty<string>())
    {
    }
}
