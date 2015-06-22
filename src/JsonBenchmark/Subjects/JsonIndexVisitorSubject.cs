using JsonBenchmark.Scenarios;
using JsonIndex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonBenchmark.Subjects
{
    public class JsonIndexVisitorSubject : Subject, ScenarioVisitor
    {
        private TimeSpan duration;

        public string Name
        {
            get { return "json-index-visitor"; }
        }

        public TimeSpan Execute(Scenario scenario)
        {
            scenario.Accept(this);
            return this.duration;
        }

        public void Visit(AllPropertiesScenario scenario)
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
                    AllPropertiesVisitor visitor = new AllPropertiesVisitor();

                    index.Root.Accept(visitor);

                    foreach (string property in visitor.Properties)
                    {
                        properties.Add(property);
                    }
                }
            });
        }

        public void Visit(AllStreetsScenario scenario)
        {
            this.duration = scenario.Execute((stream, streets) =>
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    IndexSettings settings = new IndexSettings
                    {
                        IndexFalse = false,
                        IndexTrue = false,
                        IndexNumber = false
                    };

                    Index index = IndexFactory.Build(reader.ReadToEnd(), settings);
                    AllStreetsVisitor visitor = new AllStreetsVisitor();

                    index.Root.Accept(visitor);

                    foreach (string street in visitor.Streets)
                    {
                        streets.Add(street);
                    }
                }
            });
        }

        private class AllPropertiesVisitor : JsonVisitorBase
        {
            private readonly HashSet<JsonPropertyName> properties;

            public AllPropertiesVisitor()
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

        private class AllStreetsVisitor : JsonVisitorBase
        {
            private readonly HashSet<string> streets;

            public AllStreetsVisitor()
            {
                this.streets = new HashSet<string>();
            }

            public IEnumerable<string> Streets
            {
                get { return this.streets; }
            }

            public override void Visit(JsonObject instance)
            {
                JsonProperty property = instance.Properties["STREET"] as JsonProperty;
                if (property != null)
                {
                    JsonNode value = property.GetValue();

                    if (value is JsonText)
                    {
                        streets.Add(value.ToString());
                    }
                    else if (value is JsonNull)
                    {
                        streets.Add(null);
                    }
                }

                base.Visit(instance);
            }
        }
    }
}
