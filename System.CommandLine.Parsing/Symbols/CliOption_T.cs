using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;
using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public class CliOption<T> : CliOption, IParsableSymbol<T>
{
    private T? _defaultValue;
    private bool _hasDefaultValue;

    /// <summary>
    /// Initializes a new instance of the Option class.
    /// </summary>
    /// <param name="name">The name of the option. It's used for parsing, displaying Help and creating parse errors.</param>>
    /// <param name="aliases">Optional aliases. Used for parsing, suggestions and displayed in Help.</param>
    public CliOption(string name, params string[] aliases) : base(name)
    {
        foreach (string alias in aliases) Aliases.Add(alias);
        Parser = ArgumentResult.GetDefaultParser<T>();
    }

    internal CliOption(string name, Func<ArgumentResult, T> parser, params string[] aliases) : base(name)
    {
        foreach (string alias in aliases) Aliases.Add(alias);
        Parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    /// <inheritdoc />
    public override Type ValueType => typeof(T);

    public T? DefaultValue
    {
        get => _defaultValue;
        set
        {
            _defaultValue = value;
            _hasDefaultValue = true;
        }
    }

    /// <summary>
    /// A custom argument parser.
    /// </summary>
    /// <remarks>
    /// It's invoked when there was parse input provided for given Argument.
    /// The same instance can be set as <see cref="DefaultValueFactory"/>, in such case
    /// the delegate is also invoked when no input was provided.
    /// </remarks>
    public Func<ArgumentResult, T> Parser { get; set; }

    public override bool HasDefaultValue => _hasDefaultValue;

    public override object? GetDefaultValue() => _defaultValue;
}
