using JsonBenchmark.Subjects;

namespace JsonBenchmark
{
    public interface SubjectVisitor
    {
        void Visit(NewtonsoftSubject subject);
    }
}
