using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;
using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public class CliOption<T> : CliOption, IParsableSymbol<T>
{
    private Func<ParseInput, T>? _parser;
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
    }

    internal CliOption(string name, Func<ParseInput, T> parser, params string[] aliases) : base(name)
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
    /// Argument parser.
    /// </summary>
    public Func<ParseInput, T> Parser
    {
        get => _parser ??= ParseInput.GetDefaultParser<T>();
        set => _parser = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override bool TryGetDefaultValue(out object? defaultValue)
    {
        defaultValue = _hasDefaultValue ? _defaultValue : default;
        return _hasDefaultValue;
    }
}
