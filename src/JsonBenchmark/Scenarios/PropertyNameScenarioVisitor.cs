using JsonBenchmark.Subjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonBenchmark.Scenarios
{
    public class PropertyNameScenarioVisitor : SubjectVisitor
    {
        public static readonly string[] Expected =
        {
            "type", "features", "properties", "MAPBLKLOT", "BLKLOT", "BLOCK_NUM",
            "LOT_NUM", "FROM_ST", "TO_ST", "STREET", "ST_TYPE", "ODD_EVEN",
            "geometry", "coordinates"
        };

        private TimeSpan duration;

        public TimeSpan Duration
        {
            get { return this.duration; }
        }

        public void Visit(NewtonsoftSubject subject)
        {
            Execute((stream, properties) =>
            {
                using (StreamReader streamReader = new StreamReader(stream))
                using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                {
                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.PropertyName)
                        {
                            properties.Add((string)jsonReader.Value);
                        }
                    }
                }
            });
        }

        private void Execute(Action<FileStream, ICollection<string>> context)
        {
            DateTime started;
            HashSet<string> properties = new HashSet<string>();

            using (FileStream stream = File.OpenRead("Resources\\citylots.json"))
            {
                started = DateTime.Now;
                context(stream, properties);
            }

            this.duration = DateTime.Now - started;

            if (Expected.Intersect(properties).Count() != Expected.Length)
            {
                throw new NotSupportedException();
            }
        }
    }
}
