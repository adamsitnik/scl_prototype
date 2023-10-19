using System.Collections.Generic;
using System.IO;

namespace System.CommandLine.Parsing;

/// <summary>
/// Represents the configuration used by the <see cref="CliParser"/>.
/// </summary>
public class CliConfiguration
{
    private TextWriter? _output, _error;

    /// <summary>
    /// Initializes a new instance of the <see cref="CliConfiguration"/> class.
    /// </summary>
    /// <param name="rootCommand">The root command for the parser.</param>
    public CliConfiguration(CliCommand rootCommand) => RootCommand = rootCommand ?? throw new ArgumentNullException(nameof(rootCommand));

    /// <summary>
    /// Enables the parser to recognize and expand POSIX-style bundled options.
    /// </summary>
    /// <param name="value"><see langword="true"/> to parse POSIX bundles; otherwise, <see langword="false"/>.</param>
    /// <remarks>
    /// POSIX conventions recommend that single-character options be allowed to be specified together after a single <c>-</c> prefix. When <see cref="EnablePosixBundling"/> is set to <see langword="true"/>, the following command lines are equivalent:
    /// 
    /// <code>
    ///     &gt; myapp -a -b -c
    ///     &gt; myapp -abc
    /// </code>
    /// 
    /// If an argument is provided after an option bundle, it applies to the last option in the bundle. When <see cref="EnablePosixBundling"/> is set to <see langword="true"/>, all of the following command lines are equivalent:
    /// <code>
    ///     &gt; myapp -a -b -c arg
    ///     &gt; myapp -abc arg
    ///     &gt; myapp -abcarg
    /// </code>
    ///
    /// </remarks>
    public bool EnablePosixBundling { get; set; } = true;

    // DESIGN: EnableDefaultExceptionHandler and ProcessTerminationTimeout got removed (execution is no longer part of the parser)
    // TODO: ResponseFileTokenReplacer got removed, but we need to make it possible

    /// <summary>
    /// Gets the root command.
    /// </summary>
    public CliCommand RootCommand { get; }

    /// <summary>
    /// The standard output. Used by Help and other facilities that write non-error information.
    /// By default it's set to <see cref="Console.Out"/>.
    /// For testing purposes, it can be set to a new instance of <see cref="StringWriter"/>.
    /// If you want to disable the output, please set it to <see cref="TextWriter.Null"/>.
    /// </summary>
    public TextWriter Output
    {
        get => _output ??= Console.Out;
        set => _output = value ?? throw new ArgumentNullException(nameof(value), "Use TextWriter.Null to disable the output");
    }

    /// <summary>
    /// The standard error. Used for printing error information like parse errors.
    /// By default it's set to <see cref="Console.Error"/>.
    /// For testing purposes, it can be set to a new instance of <see cref="StringWriter"/>.
    /// </summary>
    public TextWriter Error
    {
        get => _error ??= Console.Error;
        set => _error = value ?? throw new ArgumentNullException(nameof(value), "Use TextWriter.Null to disable the output");
    }

    /// <summary>
    /// Parses an array strings using the configured <see cref="RootCommand"/>.
    /// </summary>
    /// <param name="args">The string arguments to parse.</param>
    /// <returns>A parse result describing the outcome of the parse operation.</returns>
    public ParseResult Parse(IReadOnlyList<string> args)
        => CliParser.Parse(RootCommand, args, this);

    /// <summary>
    /// Parses a command line string value using the configured <see cref="RootCommand"/>.
    /// </summary>
    /// <remarks>The command line string input will be split into tokens as if it had been passed on the command line.</remarks>
    /// <param name="commandLine">A command line string to parse, which can include spaces and quotes equivalent to what can be entered into a terminal.</param>
    /// <returns>A parse result describing the outcome of the parse operation.</returns>
    public ParseResult Parse(string commandLine)
        => CliParser.Parse(RootCommand, commandLine, this);
}
