using CommandLine;

namespace App.Commands
{
    [Verb("crash", HelpText = "Simulates unhandled exception.")]
    public class CrashCommand : CommandBase
    {
        public override void Execute()
        {
            var zero = 0;
            // This will throw an error.
            var result = 42 / zero;
        }
    }
}
