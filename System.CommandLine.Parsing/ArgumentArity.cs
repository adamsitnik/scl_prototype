namespace System.CommandLine.Parsing;

/// <summary>
/// Defines the arity of an option or argument.
/// </summary>
/// <remarks>The arity refers to the number of values that can be passed on the command line.
/// </remarks>
public readonly struct ArgumentArity : IEquatable<ArgumentArity>
{
    private const int MaximumArity = 100_000;

    /// <summary>
    /// Initializes a new instance of the ArgumentArity class.
    /// </summary>
    /// <param name="minimumNumberOfValues">The minimum number of values required for the argument.</param>
    /// <param name="maximumNumberOfValues">The maximum number of values allowed for the argument.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="minimumNumberOfValues"/> is negative.</exception>
    /// <exception cref="ArgumentException">Thrown when the maximum number is less than the minimum number or the maximum number is greater than MaximumArity.</exception>
    public ArgumentArity(int minimumNumberOfValues, int maximumNumberOfValues)
    {
        MinimumNumberOfValues = minimumNumberOfValues;
        MaximumNumberOfValues = maximumNumberOfValues;
    }

    /// <summary>
    /// Gets the minimum number of values required for an <see cref="CliArgument">argument</see>.
    /// </summary>
    public int MinimumNumberOfValues { get; }

    /// <summary>
    /// Gets the maximum number of values allowed for an <see cref="CliArgument">argument</see>.
    /// </summary>
    public int MaximumNumberOfValues { get; }

    /// <summary>
    /// An arity that does not allow any values.
    /// </summary>
    public static ArgumentArity Zero => new(0, 0);

    /// <summary>
    /// An arity that may have one value, but no more than one.
    /// </summary>
    public static ArgumentArity ZeroOrOne => new(0, 1);

    /// <summary>
    /// An arity that must have exactly one value.
    /// </summary>
    public static ArgumentArity ExactlyOne => new(1, 1);

    /// <summary>
    /// An arity that may have multiple values.
    /// </summary>
    public static ArgumentArity ZeroOrMore => new(0, MaximumArity);

    /// <summary>
    /// An arity that must have at least one value.
    /// </summary>
    public static ArgumentArity OneOrMore => new(1, MaximumArity);

    public bool Equals(ArgumentArity other)
        => MaximumNumberOfValues == other.MaximumNumberOfValues && MinimumNumberOfValues == other.MinimumNumberOfValues;
}
