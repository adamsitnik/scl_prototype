namespace System.CommandLine.Parsing;

/// <summary>
/// Describes an error that occurs while parsing command line input.
/// </summary>
public sealed class ParseError
{
    internal ParseError(string message, SymbolResult? symbolResult = null)
    {
        Message = message;
        SymbolResult = symbolResult;
    }

    /// <summary>
    /// A message to explain the error to a user.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// The symbol result detailing the symbol that failed to parse and the tokens involved.
    /// </summary>
    public SymbolResult? SymbolResult { get; }
}