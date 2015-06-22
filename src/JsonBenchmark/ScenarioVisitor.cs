using JsonBenchmark.Scenarios;

namespace JsonBenchmark
{
    public interface ScenarioVisitor
    {
        void Visit(AllPropertiesScenario scenario);

        void Visit(AllStreetsScenario scenario);
    }
}
