using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using AoCTools.File;
using AoCTools.Loggers;

namespace AoCTools.Workers
{
    public class WorkerBase : IWorker
    {
        private Stopwatch _timer = new Stopwatch();
        public string[] DataLines { get; private set; }
        //TODO allow multi-part data (see D19)
        public virtual object Data { get; } = null;

        public long WorkOneStar(string dataPath, SeverityLevel logAbove)
        {
            return Work(dataPath, logAbove, WorkOneStar_Implementation);
        }

        public long WorkTwoStars(string dataPath, SeverityLevel logAbove)
        {
            return Work(dataPath, logAbove, WorkTwoStars_Implementation);
        }

        private long Work(string dataPath, SeverityLevel logAbove, Func<long> implementation)
        {
            _timer.Start();
            Setup(dataPath, logAbove);
            _timer.Stop();
            LogRawData();
            Logger.Log($"Setup ended in {_timer.Elapsed:m\\:ss\\.fff}", SeverityLevel.Medium);

            _timer.Restart();
            ProcessDataLines();
            _timer.Stop();
            LogProcessedData();
            Logger.Log($"Data processed in {_timer.Elapsed:m\\:ss\\.fff}", SeverityLevel.Medium);
            Console.WriteLine();

            _timer.Restart();
            var result = implementation();
            _timer.Stop();
            Logger.Log($"Work ended in {_timer.Elapsed:m\\:ss\\.fff}", SeverityLevel.Always);
            Console.WriteLine();

            return result;
        }

        protected virtual long WorkOneStar_Implementation()
        {
            throw new NotImplementedException();
        }

        protected virtual long WorkTwoStars_Implementation()
        {
            throw new NotImplementedException();
        }

        private void Setup(string dataPath, SeverityLevel logAbove)
        {
            Logger.ShowAboveSeverity = logAbove;
            DataLines = Reader.ReadAsLines(dataPath);
        }

        private void LogRawData()
        {
            if (Logger.ShowAboveSeverity != SeverityLevel.Never)
                return;

            var sb = new StringBuilder();
            sb.AppendLine("=== READ LINES ===");
            foreach (var line in DataLines)
                sb.AppendLine(line);

            Logger.Log(sb.ToString());
        }

        protected virtual void ProcessDataLines()
        {
            throw new NotImplementedException();
        }

        private void LogProcessedData()
        {
            if (Logger.ShowAboveSeverity != SeverityLevel.Never)
                return;

            if (Data == null)
            {
                Logger.Log("No processed data.");
            }
            else if (Data is IEnumerable dataEnumerable)
            {
                var enumerator = dataEnumerable.GetEnumerator();
                enumerator.MoveNext();
                if (enumerator.Current == null)
                {
                    Logger.Log("Empty data enumerable.");
                }
                else
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"=== {enumerator.Current.GetType().Name}'s ===");
                    try
                    {
                        // append to StringBuilder until out of enumeration
                        while (true)
                        {
                            sb.AppendLine(enumerator.Current.ToString());
                            enumerator.MoveNext();
                        }
                    }
                    catch (Exception)
                    {
                        Logger.Log(sb.ToString());
                    }
                }
            }
            else
            {
                Logger.Log(Data.ToString());
            }
        }
    }
}