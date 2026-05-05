using L_CAssignmentClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_CAssignmentClasses.Logging
{
    public class FileLogger : ILogger
    {
        private static readonly string folderPath = @"D:\SampleApplication"; 
        private static readonly string fileName = "log.txt";

        private string fullPath = Path.Combine(folderPath, fileName);

        private readonly StringBuilder _log = new StringBuilder();
        private readonly object _sync = new object();
        public void Log(string message)
        {
            var entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}";
            lock (_sync)
            {
                _log.AppendLine(entry);
            }
            Save(fullPath);
        }

        public void Save(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(directory); 
            }

            lock (_sync)
            {
                File.WriteAllText(path, _log.ToString());
            }
        }
    }
}
