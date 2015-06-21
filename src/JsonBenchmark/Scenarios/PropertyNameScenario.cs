using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace JsonBenchmark.Scenarios
{
    public class PropertyNameScenario : Scenario
    {
        public string Name
        {
            get { return "property-name"; }
        }

        public IEnumerable<string> Properties
        {
            get
            {
                return new[]
                {
                    "type", "features", "properties", "MAPBLKLOT", "BLKLOT", "BLOCK_NUM",
                    "LOT_NUM", "FROM_ST", "TO_ST", "STREET", "ST_TYPE", "ODD_EVEN",
                    "geometry", "coordinates"
                };
            }
        }

        public TimeSpan Execute(Action<Stream, ICollection<string>> context)
        {
            DateTime started = DateTime.Now;
            HashSet<string> properties = new HashSet<string>();

            using (ZipArchive archive = ZipFile.OpenRead("Resources\\citylots.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    using (Stream stream = entry.Open())
                    {
                        context(stream, properties);
                    }
                }
            }

            if (Properties.Intersect(properties).Count() != Properties.Count())
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
