using System.Collections.Generic;

namespace System.CommandLine.Parsing;

/// <summary>
/// A result produced when parsing a <see cref="CliCommand" />.
/// </summary>
public sealed class CommandResult : SymbolResult
{
    internal CommandResult(CliCommand command, CliToken token, CommandResult? parent = null) : base(parent)
    {
        Command = command;
        IdentifierToken = token;
    }

    /// <summary>
    /// The command to which the result applies.
    /// </summary>
    public CliCommand Command { get; }

    /// <summary>
    /// The token that was parsed to specify the command.
    /// </summary>
    public CliToken IdentifierToken { get; }

    /// <summary>
    /// Child symbol results in the parse tree.
    /// </summary>
    public IEnumerable<SymbolResult> Children => throw new NotImplementedException();
}