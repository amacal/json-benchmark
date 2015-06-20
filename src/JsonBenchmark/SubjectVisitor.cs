﻿using JsonBenchmark.Subjects;

namespace JsonBenchmark
{
    public interface SubjectVisitor
    {
        void Visit(JsonIndexVisitorSubject subject);

        void Visit(NewtonsoftReaderSubject subject);
    }
}
