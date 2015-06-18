namespace JsonBenchmark
{
    public interface Subject
    {
        string Name { get; }

        void Accept(SubjectVisitor visitor);
    }
}
