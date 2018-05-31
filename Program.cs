using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Andrzej
{
    class Program
    {
        static readonly string _logFilePath = @"C:\Users\Edzia\Documents\Andrzej\Power.txt";
        static string _generalCaptureRegex = @"^.*([0-9]{2}:[0-9]{2}:[0-9]{2}).*GameState\.DebugPrintPower\(\) *\- *([A-Za-z0-9\[\]_= -]*)";
        static string _tagCaptureRegex = @"tag=([A-Za-z0-9_-]*) *value=([A-Za-z0-9]*)";
        static void Main(string[] args)
        {
            var logFile = File.ReadAllLines(_logFilePath);

            foreach (var logLine in logFile)
            {
                if (logLine == string.Empty || logFile.Length == 1)
                    continue;

                var logRegexMatch = Regex.Match(logLine, _generalCaptureRegex);
                if (logRegexMatch.Groups.Count <= 1)
                    continue;

                var logDatetime = logRegexMatch.Groups[1].Value;
                var logEvent = logRegexMatch.Groups[2].Value;

                var entityStateChangeTest = Regex.Match(logEvent, _tagCaptureRegex);

                if (entityStateChangeTest.Success && entityStateChangeTest.Groups.Count == 3)
                    Console.WriteLine(string.Format("\t{0}:{1}", entityStateChangeTest.Groups[1].Value, entityStateChangeTest.Groups[2].Value));
                else
                    Console.WriteLine(string.Format(@"{0} {1}", logRegexMatch.Groups[1].Value, logRegexMatch.Groups[2].Value));
            }
        }
    }
}
