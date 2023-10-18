using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public class CliArgument<T> : CliArgument
{
    /// <summary>
    /// Initializes a new instance of the Argument class.
    /// </summary>
    /// <param name="name">The name of the argument. It's not used for parsing, only when displaying Help or creating parse errors.</param>>
    public CliArgument(string name) : base(name) { }

    /// <inheritdoc />
    public override Type ValueType => typeof(T);
}
