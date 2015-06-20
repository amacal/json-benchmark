using JsonBenchmark.Subjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonBenchmark.Scenarios
{
    public class PropertyNameScenario : Scenario
    {
        public string Name
        {
            get { return "property-name"; }
        }

        public TimeSpan Execute(Subject subject)
        {
            PropertyNameScenarioVisitor visitor = new PropertyNameScenarioVisitor();
            subject.Accept(visitor);
            return visitor.Duration;
        }
    }
}
