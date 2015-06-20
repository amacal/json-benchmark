namespace JsonBenchmark.Subjects
{
    public class NewtonsoftReaderSubject : Subject
    {
        public string Name
        {
            get { return "newtonsoft-reader"; }
        }

        public void Accept(SubjectVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
