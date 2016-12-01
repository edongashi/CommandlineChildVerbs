using System;
using System.Collections.Generic;
using App.Commands;
using App.Modules;
using CommandLine;
using Ninject;

// Examples:

// app --help
// app help

// app commit --help
// app commit add
// app commit add --help
// app commit log --help
// app commit log all
// app commit log all -v
// app commit log all --help
// app commit log latest
// app commit log latest --help

// app echo Hello
// app echo --help

// app now
// app now --help

// app crash
// app crash -v

namespace App
{
    public class Program
    {
        private static IKernel kernel;
        private static int returnValue;

        public static int Main(string[] args)
        {
            // Composition root is here... we load the injector and modules
            // Business behavior is determined by modules, so commands stay loosely coupled.
            kernel = new StandardKernel(new AppModule());
            try
            {
                ProcessArgs(args);
                return returnValue;
            }
            finally
            {
                kernel.Dispose();
            }
        }

        private static void ProcessArgs(IEnumerable<string> args)
        {
            try
            {
                // We can use generic helpers or pass the command types manually.
                Parser.Default.ParseVerbs<CommitCommand, EchoCommand, CurrentTimeCommand, CrashCommand>(args)
                    .WithParsed(ExecuteCommand)
                    .WithNotParsed(ParseError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parser exception: {ex.Message}");
            }
        }

        private static void ExecuteCommand(object arg)
        {
            var command = (CommandBase)arg;
            try
            {
                // The kernel will inject dependencies to the command.
                kernel.Inject(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to resolve command dependencies: {ex.Message}");
                returnValue = 1;
                return;
            }

            try
            {
                // Actual work is done here.
                command.Execute();
            }
            catch (Exception ex)
            {
                // Ideally this code should never execute.
                Console.WriteLine(ex.Message);
                if (command.Verbose)
                {
                    Console.WriteLine(Environment.NewLine + "=== Exception ===");
                    Console.WriteLine(ex.ToString());
                }

                returnValue = 1;
            }
        }

        private static void ParseError(IEnumerable<Error> obj) => returnValue = 1;
    }
}
