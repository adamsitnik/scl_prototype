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

    // DESIGN: all Find* and Get* methods got removed for clarity
    // DESIGN: AddError got moved to ParseInput so the SymbolResult can be immutable
    // TODO: how can we access other symbol parsed value during validation
}
