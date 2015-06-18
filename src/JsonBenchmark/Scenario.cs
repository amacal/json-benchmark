using System;

namespace JsonBenchmark
{
    public interface Scenario
    {
        string Name { get; }

        TimeSpan Execute(Subject subject);
    }
}
