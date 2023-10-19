namespace System.CommandLine.Parsing;

/// <summary>
/// Identifies the type of a <see cref="CliToken"/>.
/// </summary>
public enum TokenType
{
    /// <summary>
    /// An argument token.
    /// </summary>
    /// <see cref="CliArgument"/>
    Argument,

    /// <summary>
    /// A command token.
    /// </summary>
    /// <see cref="CliCommand"/>
    Command,

    /// <summary>
    /// An option token.
    /// </summary>
    /// <see cref="CliOption"/>
    Option,

    /// <summary>
    /// A double dash (<c>--</c>) token, which changes the meaning of subsequent tokens.
    /// </summary>
    DoubleDash,

    /// <summary>
    /// A directive token.
    /// </summary>
    /// <see cref="CliDirective"/>
    Directive
}
