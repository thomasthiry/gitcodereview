using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GitCodeReview
{
    public class ReviewRepository
    {
        private const string ReviewFileName = "reviews.json";

        public void SaveReviews(IEnumerable<Review> reviews)
        {
            var json = JsonConvert.SerializeObject(reviews, new JsonSerializerSettings { Formatting = Formatting.Indented });
            File.WriteAllText(ReviewFileName, json);
        }

        public IEnumerable<Review> GetReviews()
        {
            if (File.Exists(ReviewFileName) == false)
            {
                return new List<Review>();
            }
            var json = File.ReadAllText(ReviewFileName);
            var reviews = JsonConvert.DeserializeObject<IEnumerable<Review>>(json);

            return reviews;
        }
    }
}