// DESIGN: not included in System.CommandLine namespace since most of the users won't ever need to use CliArgument type
namespace System.CommandLine.Symbols;

/// <summary>
/// A symbol defining a value that can be passed on the command line to a <see cref="CliCommand">command</see> or <see cref="CliOption">option</see>.
/// </summary>
public abstract class CliArgument : CliSymbol
{
    private protected CliArgument(string name) : base(name) { }

    // DESIGN: HelpName is not included, as Help was moved out of main package

    /// <summary>
    /// Gets or sets the <see cref="Type" /> that the argument token(s) will be converted to.
    /// </summary>
    public abstract Type ValueType { get; }
}
