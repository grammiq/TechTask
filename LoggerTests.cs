using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.IO;

// Task 1.1
namespace LoggerTests
{
    public class LoggerTests
    {
        public string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public readonly string logFile = "application.txt";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(Path.Combine(docPath, logFile)))
            {
                File.Delete(Path.Combine(docPath, logFile));
            }
        }

        [Test]
        [TestCase("User logged in", "INFO")]
        [TestCase("Failed login attempt", "WARNING")]
        public void Check_LogMessage_Writes_LogEntry_To_LogFile(string logEvent, string logType)
        {
            Logger.log_message(logFile, logEvent, logType);
            
            string[] logEntries = File.ReadAllLines(Path.Combine(docPath, logFile));
            Assert.That(logEntries.Length, Is.EqualTo(1));
            StringAssert.Contains($"[{logType}] [{logEvent}]", logEntries[0]);
        }

        [Test]
        [TestCase("User logged in", "INFO")]
        public void Check_LogMessage_Writes_Exact_Timestamp_To_LogFile(string logEvent, string logType)
        {
            string expectedTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Logger.log_message(logFile, logEvent, logType);

            string[] logEntries = File.ReadAllLines(Path.Combine(docPath, logFile));
            StringAssert.Contains($"[{expectedTimeStamp}]", logEntries[0]);
        }

        [Test]
        [TestCase("User logged in", "INFO", "Failed login attempt", "WARNING")]
        public void Check_LogMessage_Writes_Several_Records_To_LogFile(string logEvent1, string logType1, string logEvent2, string logType2)
        {
            Logger.log_message(logFile, logEvent1, logType1);
            Logger.log_message(logFile, logEvent2, logType2);

            string[] logEntries = File.ReadAllLines(Path.Combine(docPath, logFile));
            Assert.That(logEntries.Length, Is.EqualTo(2));
        }
    }
}