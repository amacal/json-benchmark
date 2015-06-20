using JsonBenchmark.Subjects;
using JsonIndex;
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

        public void Visit(JsonIndexEnumeratorSubject subject)
        {
            Execute((stream, properties) =>
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    IndexSettings settings = new IndexSettings
                    {
                        IndexFalse = false,
                        IndexNull = false,
                        IndexTrue = false,
                        IndexNumber = false,
                        IndexText = false
                    };

                    Index index = IndexFactory.Build(reader.ReadToEnd(), settings);
                    HashSet<JsonPropertyName> names = new HashSet<JsonPropertyName>();

                    foreach (JsonProperty property in Flatten(index.Root).OfType<JsonProperty>())
                    {
                        if (names.Add(property.Name) == true)
                        {
                            properties.Add(property.Name.Value);
                        }
                    }
                }
            });
        }

        public void Visit(JsonIndexVisitorSubject subject)
        {
            Execute((stream, properties) =>
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    IndexSettings settings = new IndexSettings
                    {
                        IndexFalse = false,
                        IndexNull = false,
                        IndexTrue = false,
                        IndexNumber = false,
                        IndexText = false
                    };

                    Index index = IndexFactory.Build(reader.ReadToEnd(), settings);
                    JsonIndexVisitor visitor = new JsonIndexVisitor();

                    index.Root.Accept(visitor);

                    foreach (string property in visitor.Properties)
                    {
                        properties.Add(property);
                    }
                }
            });
        }

        public void Visit(NewtonsoftReaderSubject subject)
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

        private class JsonIndexVisitor : JsonVisitorBase
        {
            private readonly HashSet<JsonPropertyName> properties;

            public JsonIndexVisitor()
            {
                this.properties = new HashSet<JsonPropertyName>();
            }

            public IEnumerable<string> Properties
            {
                get { return this.properties.Select(x => x.Value); }
            }

            public override void Visit(JsonProperty property)
            {
                this.properties.Add(property.Name);
                base.Visit(property);
            }
        }

        private static IEnumerable<JsonNode> Flatten(JsonNode node)
        {
            yield return node;

            foreach (JsonNode child in node.GetChildren())
            {
                foreach (JsonNode item in Flatten(child))
                {
                    yield return item;
                }
            }
        }
    }
}
