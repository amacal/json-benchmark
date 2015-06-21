using JsonBenchmark.Scenarios;
using JsonIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonBenchmark.Subjects
{
    public class JsonIndexEnumeratorSubject : Subject, ScenarioVisitor
    {
        private TimeSpan duration;

        public string Name
        {
            get { return "json-index-enumerator"; }
        }

        public TimeSpan Execute(Scenario scenario)
        {
            scenario.Accept(this);
            return this.duration;
        }

        public void Visit(PropertyNameScenario scenario)
        {
            this.duration = scenario.Execute((stream, properties) =>
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
