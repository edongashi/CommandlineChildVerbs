using App.Services;
using CommandLine;
using Ninject;

namespace App.Commands
{
    /// <summary>
    /// Base class that serves as an interface for execution 
    /// and provides common properties and components for commands.
    /// </summary>
    /// <remarks>
    /// General pattern is that commands that have child verbs should be left abstract
    /// and provide utilities, while leaf commands that do the actual work should be concrete.
    /// </remarks>
    public abstract class CommandBase
    {
        // Properties shared by all commands...
        [Option('v', "verbose", HelpText = "Print details during execution.")]
        public bool Verbose { get; set; }
        
        // Components shared by all commands... you can inject a database connection etc.
        [Inject]
        public ILogWriter LogWriter { get; set; }
        
        // Methods shared by all commands...
        protected void WriteLog(string value) => LogWriter?.Log(value);

        protected void WriteError(string value) => LogWriter?.Error(value);

        protected void WriteVerbose(string value)
        {
            if (Verbose)
            {
                LogWriter?.Log(value);
            }
        }
        
        // Interface by which we execute our commands.
        public abstract void Execute();
    }
}
