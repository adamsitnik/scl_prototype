using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;
using System.CommandLine.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public class CliArgument<T> : CliArgument, IParsableSymbol<T>
{
    private T? _defaultValue;
    private bool _hasDefaultValue;

    /// <summary>
    /// Initializes a new instance of the Argument class.
    /// </summary>
    /// <param name="name">The name of the argument. It's not used for parsing, only when displaying Help or creating parse errors.</param>>
    public CliArgument(string name) : base(name) => Parser = ArgumentResult.GetDefaultParser<T>();

    internal CliArgument(string name, Func<ArgumentResult, T> parser) : base(name)
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
