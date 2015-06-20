namespace JsonBenchmark.Subjects
{
    public class JsonIndexSubject : Subject
    {
        public string Name
        {
            get { return "json-index"; }
        }

        public void Accept(SubjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
