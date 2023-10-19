using System.Collections.Generic;
using System.CommandLine.Symbols;

namespace System.CommandLine.Parsing;

/// <summary>
/// A result produced when parsing an <see cref="CliDirective"/>.
/// </summary>
public sealed class DirectiveResult : SymbolResult
{
    internal DirectiveResult(CliDirective directive, CliToken token) : base(null)
    {
        Directive = directive;
        IdentifierToken = token;
    }

    /// <summary>
    /// Parsed values of [name:value] directive(s).
    /// </summary>
    /// <remarks>Can be empty for [name] directives.</remarks>
    public IReadOnlyList<string> Values => throw new NotImplementedException();

    /// <summary>
    /// The directive to which the result applies.
    /// </summary>
    public CliDirective Directive { get; }

    /// <summary>
    /// The token that was parsed to specify the directive.
    /// </summary>
    public CliToken IdentifierToken { get; }
}
