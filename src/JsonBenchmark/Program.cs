using System;

namespace JsonBenchmark
{
    public static class Program
    {
        public static void Main()
        {
            foreach (Scenario scenario in ScenarioFactory.All())
            {
                Console.WriteLine(scenario.Name);

                foreach (Subject subject in SubjectFactory.All())
                {
                    Console.WriteLine("    {0,-25} {1,20}", subject.Name, scenario.Execute(subject));
                }
            }
        }
    }
}
