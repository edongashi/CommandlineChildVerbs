using System;

namespace App.Services
{
    /// <summary>
    /// Standard output writer.
    /// </summary>
    public class ConsoleLogger : ILogWriter
    {
        public void Log(string value)
        {
            Console.WriteLine(value);
        }

        public void Error(string value)
        {
            Console.WriteLine(value);
        }
    }
}
