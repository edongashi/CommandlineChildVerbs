using System.Collections.Generic;

namespace App.Services
{
    public interface ICommitRepository
    {
        IEnumerable<string> GetAllCommits();

        void AddCommit(string commit);
    }
}
