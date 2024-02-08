using System.Collections.Generic;
using System.IO;
using AoCTools.Loggers;

namespace AoCTools.File
{
    public static class Reader
    {
        /// <summary>
        /// Reads given file as string lines.
        /// </summary>
        /// <param name="dataPath">Path of file to read.</param>
        /// <returns>Array of string lines.</returns>
        public static string[] ReadAsLines(string dataPath)
        {
            Logger.Log($"Reading file at {dataPath}.");
            using (var reader = new StreamReader(dataPath))
            {
                var lines = new List<string>();

                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }

                return lines.ToArray();
            }
        }
    }
}