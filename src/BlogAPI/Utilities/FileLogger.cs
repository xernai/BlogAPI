using System.IO;

namespace BlogAPI.Utilities
{
    public class FileLogger : IFileLogger
    {
       public string Path
       {

         get; set;

       }

       public void Log(string message)
        {

            File.WriteAllText(Path, message);

        }
    }
}