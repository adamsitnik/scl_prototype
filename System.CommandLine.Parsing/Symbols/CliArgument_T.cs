using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;
using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public class CliArgument<T> : CliArgument, IParsableSymbol<T>
{
    private Func<ParseInput, T>? _parser;
    private T? _defaultValue;
    private bool _hasDefaultValue;

    /// <summary>
    /// Initializes a new instance of the Argument class.
    /// </summary>
    /// <param name="name">The name of the argument. It's not used for parsing, only when displaying Help or creating parse errors.</param>>
    public CliArgument(string name) : base(name) { }

    internal CliArgument(string name, Func<ParseInput, T> parser) : base(name)
    {
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