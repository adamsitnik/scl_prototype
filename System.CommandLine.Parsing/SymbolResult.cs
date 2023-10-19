using System.Collections.Generic;

namespace System.CommandLine.Parsing;

/// <summary>
/// A result produced during parsing for a specific symbol.
/// </summary>
public abstract class SymbolResult
{
    // SymbolResultTree is an internal type
    private protected SymbolResult(SymbolResult? parent) => Parent = parent;

    /// <summary>
    /// The parent symbol result in the parse tree.
    /// </summary>
    public SymbolResult? Parent { get; }

    /// <summary>
    /// The list of tokens associated with this symbol result during parsing.
    /// </summary>
    public IReadOnlyList<CliToken> Tokens => throw new NotImplementedException();

    /// <summary>
    /// Adds an error message for this symbol result to it's parse tree.
    /// </summary>
    /// <remarks>Setting an error will cause the parser to indicate an error for the user and prevent invocation of the command line.</remarks>
    public virtual void AddError(string errorMessage) { }

    // DESIGN: all Find* and Get* methods got removed for clarity
    // TODO: how can we access other symbol parsed value during validation
}
