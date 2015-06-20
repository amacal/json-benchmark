namespace JsonBenchmark.Subjects
{
    public class NewtonsoftSubject : Subject
    {
        public string Name
        {
            get { return "newtonsoft"; }
        }

        public void Accept(SubjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
