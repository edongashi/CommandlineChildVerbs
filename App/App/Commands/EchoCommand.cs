using CommandLine;

namespace App.Commands
{
    [Verb("echo", HelpText = "Echo input to console.")]
    public class EchoCommand : CommandBase
    {
        [Value(0, HelpText = "Input text.", Required = true)]
        public string Input { get; set; }

        public override void Execute()
        {
            WriteLog(Input);
        }
    }
}
