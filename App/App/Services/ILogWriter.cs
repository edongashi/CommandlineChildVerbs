namespace App.Services
{
    /// <summary>
    /// Abstract logging interface so commands are not bound to console only output.
    /// </summary>
    public interface ILogWriter
    {
        void Log(string value);

        void Error(string value);
    }
}