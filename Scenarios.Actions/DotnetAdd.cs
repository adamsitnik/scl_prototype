using System.CommandLine;

namespace Scenarios.Actions
{
    internal static class DotnetAdd
    {
        static int GetWithHighLevelApi(string[] args)
        {
            var builder = CommandLineApplication.CreateBuilder();

            CliArgument<FileInfo> projectArg = new("project")
            {
                Description = "The project file to operate on. If a file is not specified, the command will search the current directory for one.",
            };

            CliCommand addCommand = new("add", ".NET Add Command")
            {
                projectArg
            };

            // this particular command has no actions
            builder.RootCommand.Subcommands.Add(addCommand);

            CliOption<bool> interactiveOption = new("--interactive")
            {
                Description = "Allows the command to stop and wait for user input or action(for example to complete authentication)."
            };
            CliOption<string> frameworkOption = new("--framework", "-f")
            {
                Description = "Add the reference only when targeting a specific framework."
            };

            CliCommand addReferenceCommand = builder.AddCommand("reference",
                projectArg,
                new CliArgument<FileInfo>("projectPath")
                {
                    Description = "The paths to the projects to add as references"
                },
                frameworkOption,
                interactiveOption,
                (project, projectPath, framework, interactive) =>
                {
                    Console.WriteLine("dotnet add reference");
                },
                parent: addCommand);
            addReferenceCommand.Description = "Add a project-to-project reference to the project.";

            CliCommand addPackageCommand = builder.AddCommand("package",
                projectArg,
                new CliArgument<string>("packageName")
                {
                    Description = "The package reference to add."
                },
                new CliOption<string>("--version", "-v")
                {
                    Description = "The version of the package to add."
                },
                frameworkOption,
                new CliOption<bool>("--no-restore", "-n")
                {
                    Description = "Add the reference without performing restore preview and compatibility check."
                },
                new CliOption<string>("--source", "-s")
                {
                    Description = "The NuGet package source to use during the restore."
                },
                new CliOption<DirectoryInfo>("--package-directory")
                {
                    Description = "The directory to restore packages to."
                },
                interactiveOption,
                new CliOption<bool>("--prerelease")
                {
                    Description = "Allows prerelease packages to be installed."
                },
                (project, packageNAme, version, framework, noRestore, source, packageDir, interactive, prerelease) =>
                {
                    Console.WriteLine("dotnet add package");
                },
                parent: addCommand);
            addPackageCommand.Description = "Add a NuGet package reference to the project.";

            return builder.Build().Run(args);
        }
    }
}
