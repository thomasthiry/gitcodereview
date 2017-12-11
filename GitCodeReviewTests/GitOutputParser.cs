using System;
using System.IO;
using System.Linq;
using GitCodeReview;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace GitCodeReviewTests
{
    [TestClass]
    public class GitOutputParserTests
    {
        [TestMethod]
        public void ParseGitOutput()
        {
            var gitOutput = File.ReadAllText("GitOutput1.txt");

            var commits = GitOutputParser.ParseGitOutput(gitOutput);

            commits.Count().ShouldBeGreaterThan(1);
        }
    }
}
