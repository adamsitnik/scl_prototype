using System.CommandLine.Parsing;
using System.CommandLine.Parsing.Symbols;

// DESIGN: this type will be frequently used and that it's why in the main namespace
namespace System.CommandLine;

public static class Validators
{
    // DESIGN: this allows for adding the validators outside of the main package
    // PROS: thanks to the IParsableSymbol we could finally reuse Argument and Option validators
    // CONS:
    // - it could be misused by setting custom parser after validator
    // - there would be no way to remove custom or default validators
    // TODO: would that be acceptable?
    public static void AddValidator<T>(this IParsableSymbol<T> parsableSymbol, Action<ParseInput, T> validator)
    {
        Func<ParseInput, T> actualParser = parsableSymbol.Parser;

        parsableSymbol.Parser = parseInput =>
        {
            T parsed = actualParser.Invoke(parseInput);

            validator.Invoke(parseInput, parsed);

            return parsed;
        };
    }
}