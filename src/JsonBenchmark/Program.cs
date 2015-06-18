using System;

namespace JsonBenchmark
{
    public static class Program
    {
        public static void Main()
        {
            foreach (Scenario scenario in ScenarioFactory.All())
            {
                foreach (Subject subject in SubjectFactory.All())
                {
                    Console.WriteLine("{0,-20} {1,20}", subject.Name, scenario.Execute(subject));
                }
            }
        }
    }
}
