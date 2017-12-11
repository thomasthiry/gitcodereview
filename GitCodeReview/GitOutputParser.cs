using System;
using System.Collections.Generic;

namespace GitCodeReview
{
    public static class GitOutputParser
    {
        public static IEnumerable<GitCommit> ParseGitOutput(string output)
        {
            var commits = new List<GitCommit>();
            foreach (var line in output.Replace("\r", "").Split('\n'))
            {
                var parts = line.Split('~');
                if (parts.Length == 4)
                {
                    commits.Add(new GitCommit
                    {
                        Hash = parts[0],
                        Author = parts[1],
                        Created = DateTimeOffset.Parse(parts[2]),
                        Comment = parts[3]
                    });
                }
            }
            return commits;
        }
    }
}