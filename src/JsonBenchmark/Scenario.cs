namespace JsonBenchmark
{
    public interface Scenario
    {
        string Name { get; }

        void Accept(ScenarioVisitor visitor);
    }
}
