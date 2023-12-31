﻿using System.CommandLine;
using System.CommandLine.Parsing;
using System.CommandLine.Routing;
using System.Net.NetworkInformation;

namespace Scenarios.Actions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        static int GetByReference(string[] args)
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

            SymbolRouter app = new();
            app.Use(command, parseResult =>
            {
                string target = parseResult.GetValue(targetArg)!;
                int count = parseResult.GetValue(countOption);

                Ping(target, count);
            });

            ParseResult parseResult = command.Parse(args);
            if (app.TryGetSynchronousAction(parseResult.TerminatingSymbolResult, out var action))
            {
                return action!.Invoke(parseResult);
            }
            return -1;
        }

        static int GetByName(string[] args)
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

            SymbolRouter app = new();
            app.Use(command, parseResult =>
            {
                string target = parseResult.GetValue<string>("target")!;
                int count = parseResult.GetValue<int>("-n");

                Ping(target, count);
            });

            ParseResult parseResult = command.Parse(args);
            if (app.TryGetSynchronousAction(parseResult.TerminatingSymbolResult, out var action))
            {
                return action!.Invoke(parseResult);
            }
            return -1;
        }

        static int GetWithHighLevelApi(string[] args)
        {
            var builder = CommandLineApplication.CreateBuilder();
            builder.AddCommand("ping",
                new CliArgument<string>("target"),
                new CliOption<int>("-n")
                {
                    DefaultValue = 4,
                    Description = "Number of echo requests to send."
                },
                (target, count) => Ping(target!, count));

            return builder.Build().Run(args);
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
