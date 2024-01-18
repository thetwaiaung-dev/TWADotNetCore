using Serilog;

namespace TWADotNetCore.MVC.Helpers
{
    public class LogHelper
    {
        public void Info(string message)
        {
            Log.Information(message);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }
    }
}
