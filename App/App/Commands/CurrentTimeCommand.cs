using System;
using CommandLine;

namespace App.Commands
{
    [Verb("now", HelpText = "Display current time.")]
    public class CurrentTimeCommand : CommandBase
    {
        public override void Execute()
        {
            WriteLog(DateTime.Now.ToString());
        }
    }
}
