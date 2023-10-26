using System.CommandLine.Parsing;
using System.CommandLine.Routing;
using System.Threading;
using System.Threading.Tasks;

// DESIGN: very frequent use
namespace System.CommandLine
{
    // DESIGN: mimic what WebApplication does
    public class CommandLineApplication
    {
        internal CommandLineApplication(CliCommand rootCommand, SymbolRouter map)
        {
            RootCommand = rootCommand;
            Map = map;
        }

        public CliCommand RootCommand { get; }

        // DESIGN: I don't see the point of exposing it
        private SymbolRouter Map { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineApplicationBuilder"/> class with preconfigured defaults.
        /// </summary>
        public static CommandLineApplicationBuilder CreateBuilder() => new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineApplicationBuilder"/> class for provided root command.
        /// </summary>
        public static CommandLineApplicationBuilder CreateBuilder(CliCommand rootCommand) => new(rootCommand);

        public int Run(string[] args)
        {
            ParseResult parseResult = RootCommand.Parse(args);

            if (parseResult.Errors.Count > 0)
            {
                // TODO should we print errors during parsing or here?
                return -1;
            }

            if (!Map.TryGetSynchronousAction(parseResult.TerminatingSymbolResult, out var action))
            {
                // TODO: print an error
                return -1;
            }

            try
            {
                return RunSynchronous(parseResult, action!);
            }
            catch (Exception)
            {
                // TODO: log exception here
                throw;
            }
        }

        public async Task<int> RunAsync(string[] args, CancellationToken cancellationToken)
        {
            ParseResult parseResult = RootCommand.Parse(args);

            if (parseResult.Errors.Count > 0)
            {
                // TODO should we print errors during parsing or here?
                return -1;
            }

            try
            {
                if (Map.TryGetSynchronousAction(parseResult.TerminatingSymbolResult, out var action))
                {
                    return RunSynchronous(parseResult, action!);
                }
                else if (Map.TryGetAsynchronousAction(parseResult.TerminatingSymbolResult, out var asyncAction))
                {
                    return await asyncAction!.Invoke(parseResult, cancellationToken);
                }
                else
                {
                    // TODO: print an error
                    return -1;
                }
            }
            catch (Exception)
            {
                // TODO: log exception here
                throw;
            }
        }

        private static int RunSynchronous(ParseResult parseResult, Func<ParseResult, int> action)
        {
            try
            {
                action.Invoke(parseResult);
                return 0;
            }
            catch (Exception)
            {
                // TODO: log error
                return -1;
            }
        }
    }
}
