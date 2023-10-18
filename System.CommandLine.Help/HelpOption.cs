namespace System.CommandLine.Help;

public sealed class HelpOption : CliOption<bool>
{
    /// <summary>
    /// When added to a <see cref="CliCommand"/>, it configures the application to show help when one of the following options are specified on the command line:
    /// <code>
    ///    -h
    ///    /h
    ///    --help
    ///    -?
    ///    /?
    /// </code>
    /// </summary>
    public HelpOption() : base("--help", new[] { "-h", "/h", "-?", "/?" })
    {
        Recursive = true;
        Terminating = true;
    }
}
