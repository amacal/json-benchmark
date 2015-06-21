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
                    PropertyNameScenarioVisitor visitor = new PropertyNameScenarioVisitor();

                    index.Root.Accept(visitor);

                    foreach (string property in visitor.Properties)
                    {
                        properties.Add(property);
                    }
                }
            });
        }

        private class PropertyNameScenarioVisitor : JsonVisitorBase
        {
            private readonly HashSet<JsonPropertyName> properties;

            public PropertyNameScenarioVisitor()
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
    }
}
