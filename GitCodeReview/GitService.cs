using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace GitCodeReview
{
    public class GitService
    {
        private IEnumerable<GitCommit> _commits;
        private readonly ReviewRepository _reviewRespository;

        public GitService()
        {
            _reviewRespository = new ReviewRepository();
        }

        public IEnumerable<GitCommit> GetCommits()
        {
            if (_commits == null)
            {
                var output = ExecuteGitLog();
                _commits = GitOutputParser.ParseGitOutput(output);

                var reviews = _reviewRespository.GetReviews();

                UpdateReviewedCommits(_commits, reviews);
            }

            return _commits;
        }

        private void UpdateReviewedCommits(IEnumerable<GitCommit> commits, IEnumerable<Review> reviews)
        {
            foreach (var commit in commits)
            {
                var review = reviews.FirstOrDefault(r => r.Hash == commit.Hash);
                commit.Reviewed = review?.Reviewed ?? false;
            }
        }

        private static string ExecuteGitLog()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = @"E:\Development\MaltAndMout";
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C git log --branches --pretty=format:\"%H~%an~%aI~%s\"";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            process.StartInfo = startInfo;
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }

        public void ToggleCommitReviewed(GitCommit commitToUpdate)
        {
            var commit = _commits.First(c => c.Hash == commitToUpdate.Hash);
            commit.Reviewed = !commit.Reviewed;

            var reviews = _commits.Select(c => new Review {Hash = c.Hash, Reviewed = c.Reviewed});

            _reviewRespository.SaveReviews(reviews);
        }
    }
}