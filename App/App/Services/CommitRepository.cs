using System.Collections.Generic;

namespace App.Services
{
    public class CommitRepository : ICommitRepository
    {
        public IEnumerable<string> GetAllCommits()
        {
            return new[]
            {
                "1f6ca456f5a77b8f2d50",
                "50206dd0661ba41c1b57",
                "24af968e4b669475559f",
                "761d101c9a1b7977f035",
                "c692df172526be53cae2",
                "08ebf589ab021bcb75f6",
                "d4ca5a19e5687bd8d1cc"
            };
        }

        public void AddCommit(string commit)
        {
            // Add commit...
        }
    }
}
