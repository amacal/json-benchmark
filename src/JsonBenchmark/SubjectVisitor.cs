using JsonBenchmark.Subjects;

namespace JsonBenchmark
{
    public interface SubjectVisitor
    {
        void Visit(JsonIndexSubject subject);

        void Visit(NewtonsoftSubject subject);
    }
}
