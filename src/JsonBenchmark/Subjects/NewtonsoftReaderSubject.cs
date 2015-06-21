using JsonBenchmark.Scenarios;
using Newtonsoft.Json;
using System;
using System.IO;

namespace JsonBenchmark.Subjects
{
    public class NewtonsoftReaderSubject : Subject, ScenarioVisitor
    {
        private TimeSpan duration;

        public string Name
        {
            get { return "newtonsoft-reader"; }
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
    }
}
