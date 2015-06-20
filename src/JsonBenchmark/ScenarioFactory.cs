using JsonBenchmark.Scenarios;
using System.Collections.Generic;

namespace JsonBenchmark
{
    public static class ScenarioFactory
    {
        public static IEnumerable<Scenario> All()
        {
            yield return new PropertyNameScenario();
        }
    }
}
