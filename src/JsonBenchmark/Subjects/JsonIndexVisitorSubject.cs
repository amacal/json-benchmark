namespace JsonBenchmark.Subjects
{
    public class JsonIndexVisitorSubject : Subject
    {
        public string Name
        {
            get { return "json-index-visitor"; }
        }

        public void Accept(SubjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
