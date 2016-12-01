using App.Services;
using Ninject.Modules;

namespace App.Modules
{
    /// <summary>
    /// Binds components which will be injected to commands.
    /// </summary>
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogWriter>().To<ConsoleLogger>();
            Bind<ICommitRepository>().To<CommitRepository>();
        }
    }
}
