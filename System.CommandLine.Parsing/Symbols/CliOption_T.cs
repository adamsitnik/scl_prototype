using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public class CliOption<T> : CliOption
{
    /// <summary>
    /// Initializes a new instance of the Option class.
    /// </summary>
    /// <param name="name">The name of the option. It's used for parsing, displaying Help and creating parse errors.</param>>
    /// <param name="aliases">Optional aliases. Used for parsing, suggestions and displayed in Help.</param>
    public CliOption(string name, params string[] aliases) : base(name)
    {
        foreach (string alias in aliases) Aliases.Add(alias);
    }

    /// <inheritdoc />
    public override Type ValueType => typeof(T);
}
