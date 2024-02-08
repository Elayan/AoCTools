using System;
using System.Diagnostics;

namespace AoCTools.Loggers
{
    public static class Logger
    {
        public static SeverityLevel ShowAboveSeverity = SeverityLevel.Never;

        public static void Log(string message, SeverityLevel severity = SeverityLevel.Low)
        {
            if (severity < ShowAboveSeverity)
                return;

            var callerName = new StackFrame(1, false).GetMethod().Name;
            Console.WriteLine($"[{callerName}] {message}");
        }
    }
}