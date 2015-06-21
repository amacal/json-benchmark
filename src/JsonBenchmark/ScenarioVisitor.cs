using JsonBenchmark.Scenarios;

namespace JsonBenchmark
{
    public interface ScenarioVisitor
    {
        void Visit(PropertyNameScenario scenario);
    }
}
