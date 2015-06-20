using JsonBenchmark.Subjects;
using System.Collections.Generic;

namespace JsonBenchmark
{
    public static class SubjectFactory
    {
        public static IEnumerable<Subject> All()
        {
            yield return new NewtonsoftSubject();
        }
    }
}
