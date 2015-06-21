using System;

namespace JsonBenchmark
{
    public interface Subject
    {
        string Name { get; }

        TimeSpan Execute(Scenario scenario);
    }
}
