using System;
using System.IO;

// Task 1
    public class Logger
    {
        public static void log_message(string logFile, string logEvent, string logType)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] [{logType}] [{logEvent}]";
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter writer = new StreamWriter(Path.Combine(docPath, logFile), true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
