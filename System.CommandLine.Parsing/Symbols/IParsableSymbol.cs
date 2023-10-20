using System;
using System.Collections.Generic;
using System.Text;

namespace System.CommandLine.Parsing.Symbols;

public interface IParsableSymbol<T>
{
    Func<ParseInput, T> Parser { get; set; }
}
