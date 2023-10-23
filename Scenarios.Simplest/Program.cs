using System.CommandLine;
using System.CommandLine.Parsing;
using System.Net.NetworkInformation;

namespace Scenarios.Simplest
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        static void UseHighLevelApi(string[] args)
        {
            (string? target, int count) = CliParser.Parse("ping", args,
                new CliArgument<string>("target"),
                new CliOption<int>("-n")
                {
                    DefaultValue = 4,
                    Description = "Number of echo requests to send."
                });

            Ping(target!, count);
        }

        static void GetByName(string[] args)
        {
            CliCommand command = new("ping")
            {
                new CliArgument<string>("target"),
                new CliOption<int>("-n")
                {
                    DefaultValue = 4,
                    Description = "Number of echo requests to send."
                }
            };

            ParseResult parseResult = command.Parse(args);

            string target = parseResult.GetValue<string>("target")!;
            int count = parseResult.GetValue<int>("-n");

            Ping(target, count);
        }

        static void GetByReference(string[] args)
        {
            CliArgument<string> targetArg = new("target");
            CliOption<int> countOption = new("-n")
            {
                DefaultValue = 4,
                Description = "Number of echo requests to send."
            };
            CliCommand command = new("ping")
            {
                targetArg,
                countOption
            };

            ParseResult parseResult = command.Parse(args);

            string target = parseResult.GetValue(targetArg)!;
            int count = parseResult.GetValue(countOption);

            Ping(target, count);
        }

        private static void Ping(string target, int count)
        {
            using Ping ping = new();
            for (int i = 0; i < count; i++)
            {
                PingReply pingReply = ping.Send(target);
                Console.WriteLine($"{i}, Status: {pingReply.Status}, RoundtripTime: {pingReply.RoundtripTime}");
            }
        }
    }
}
