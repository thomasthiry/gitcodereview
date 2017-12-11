using System;

namespace GitCodeReview
{
    public class GitCommit
    {
        public string Hash { get; set; }
        public string Author { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Comment { get; set; }
        public bool Reviewed { get; set; }
    }
}