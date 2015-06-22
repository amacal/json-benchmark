using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace JsonBenchmark.Scenarios
{
    public class AllStreetsScenario : Scenario
    {
        public string Name
        {
            get { return "all-streets"; }
        }

        public int Count
        {
            get { return 1717; }
        }

        public TimeSpan Execute(Action<Stream, ICollection<string>> context)
        {
            DateTime started = DateTime.Now;
            HashSet<string> streets = new HashSet<string>();

            using (ZipArchive archive = ZipFile.OpenRead("Resources\\citylots.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (Stream stream = entry.Open())
                    {
                        context(stream, streets);
                    }
                }
            }

            if (streets.Count != this.Count)
            {
                throw new NotSupportedException();
            }

            return DateTime.Now - started;
        }

        public void Accept(ScenarioVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
