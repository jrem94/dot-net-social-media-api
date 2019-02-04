using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;

namespace SocialMediaAssessment.Services
{
    public class TagService
    {
        ApiContext db = new ApiContext();

        //Retrieves all hashtags tracked by the database.
        public HashTag[] GetTags()
        {
            var checkTags = from tag in db.HashTags
                            select tag;

            HashTag[] hashTags = checkTags.ToArray<HashTag>();

            return hashTags;
        }

        /*
         * Retrieves all (non-deleted) tweets tagged with the given hashtag label. The tweets should appear in reverse-chronological order.
         * If no hashtag with the given label exists, an error should be sent in lieu of a response.
         * A tweet is considered “tagged” by a hashtag if the tweet has content and the hashtag’s label appears in that content following a #
        */
        public Tweet[] GetTagsByLabel(string label)
        {

            var x = from tweet in db.SimpleTweets
                    where tweet.Tags != null
                    select tweet;
            
            var y = from tweet in db.Reposts
                    where tweet.Tags != null
                    select tweet;
            
            var z = from tweet in db.Replies
                    where tweet.Tags != null
                    select tweet;

            Tweet[] xtweets = x.ToArray<Tweet>();
            Tweet[] ytweets = y.ToArray<Tweet>();
            Tweet[] ztweets = z.ToArray<Tweet>();

            Tweet[] allTweets = new Tweet[(xtweets.Length + ytweets.Length + ztweets.Length)];

            Array.Copy(xtweets, allTweets, xtweets.Length);
            Array.Copy(ytweets, allTweets, ytweets.Length);
            Array.Copy(ztweets, allTweets, ztweets.Length);

            List<Tweet> taggedTweets = new List<Tweet>();

            foreach(Tweet tweet in allTweets)
            {
                if (!tweet.IsDeleted)
                {
                    List<HashTag> tags = tweet.Tags;

                    foreach (HashTag tag in tags)
                    {
                        if ($"{tag}" == label)
                        {
                            taggedTweets.Add(tweet);
                        }
                    }
                }
            }

            Tweet[] tagged = taggedTweets.ToArray();

            return tagged;
        }
    }
}