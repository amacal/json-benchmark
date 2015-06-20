namespace JsonBenchmark.Subjects
{
    public class JsonIndexEnumeratorSubject : Subject
    {
        public string Name
        {
            get { return "json-index-enumerator"; }
        }

        public void Accept(SubjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
