using System.Linq;
using App.Services;
using CommandLine;
using Ninject;

namespace App.Commands
{
    /// <summary>
    /// Base command for all commits, you can put shared components here.
    /// </summary>
    [Verb("commit", HelpText = "Manage commits.")]
    [ChildVerbs(typeof(AddCommitCommand), typeof(LogCommitsCommand))]
    public abstract class CommitCommand : CommandBase
    {
        /// <summary>
        /// The component that all commit specific commands will have.
        /// </summary>
        // Since the parser will handle instance creation, we are forced to use property injection.
        // I don't find this a problem because it gives a nice decorator 
        // to distinguish command parameter from business components.
        [Inject]
        public ICommitRepository CommitRepository { get; set; }

        // You can nest classes inside the base class, but it's not mandatory.
        [Verb("add", HelpText = "Add new commit.")]
        public class AddCommitCommand : CommitCommand
        {
            public override void Execute()
            {
                // We can use members inherited from both CommitCommand and BaseCommand.
                CommitRepository.AddCommit("12345678");
                WriteLog("Added new commit.");
            }
        }
    }

    // Going 3 levels deep might not be the best design choice, but it's here for demonstration.
    [Verb("log", HelpText = "Display commits.")]
    [ChildVerbs(typeof(LogAllCommitsCommand), typeof(LogLatestCommitsCommand))]
    public abstract class LogCommitsCommand : CommitCommand
    {
        // Nothing shared here but we still use the same pattern to allow expansion in the future.

        [Verb("all", HelpText = "Display all commits.")]
        public class LogAllCommitsCommand : LogCommitsCommand
        {
            public override void Execute()
            {
                var commits = CommitRepository.GetAllCommits().ToList();
                foreach (var commit in commits)
                {
                    WriteLog(commit);
                }

                // This outputs only if we add the -v option.
                WriteVerbose($"Logged {commits.Count} commits.");
            }
        }

        [Verb("latest", HelpText = "Display 3 latest commits.")]
        public class LogLatestCommitsCommand : LogCommitsCommand
        {
            public override void Execute()
            {
                // Taking first 3 commits.
                var commits = CommitRepository.GetAllCommits().Take(3);
                foreach (var commit in commits)
                {
                    WriteLog(commit);
                }
            }
        }
    }
}
