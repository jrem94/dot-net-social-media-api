using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;
using SocialMediaAssessment.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace SocialMediaAssessment.Services
{
    public class TweetService
    {
        ApiContext db = new ApiContext();
        UserService us = new UserService();
        ValidateService vs = new ValidateService();

        //GET

        public Tweet[] GetTweets()
        {
            var x = from tweet in db.SimpleTweets
                    select tweet;

            var y = from tweet in db.Reposts
                    select tweet;

            var z = from tweet in db.Replies
                    select tweet;

            Tweet[] xtweets = x.ToArray<Tweet>();
            Tweet[] ytweets = y.ToArray<Tweet>();
            Tweet[] ztweets = z.ToArray<Tweet>();

            Tweet[] allTweets = new Tweet[(xtweets.Length + ytweets.Length + ztweets.Length)];

            Array.Copy(xtweets, allTweets, xtweets.Length);
            Array.Copy(ytweets, allTweets, ytweets.Length);
            Array.Copy(ztweets, allTweets, ztweets.Length);

            return allTweets;
        }

        public Tweet GetTweetById(int id)
        {
            var tweets = GetTweets();

            foreach (Tweet tweet in tweets)
            {
                if (tweet.Id == id && !tweet.IsDeleted)
                {
                    return tweet;
                }
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public HashTag[] GetTweetTags(int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null)
            {
                return currentTweet.Tags.ToArray();
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        public User[] GetTweetLikes(int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null)
            {
                return currentTweet.Likes.ToArray();
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public Context GetTweetContext(int id)
        {
            var startingTweet = GetTweetById(id);
            List<Tweet> before = new List<Tweet>();
            List<Tweet> after = new List<Tweet>();
            var inReplyTo = startingTweet.InReplyTo;

            while (inReplyTo != null)
            {
                if (!inReplyTo.IsDeleted)
                {
                    before.Add(inReplyTo);
                }
                inReplyTo = inReplyTo.InReplyTo;
            }

            foreach(Tweet reply in startingTweet.Replies)
            {
                if (!reply.IsDeleted)
                {
                    after.Add(reply);
                }
            }

            Context tc = new Context(startingTweet, before, after);

            return tc;
        }

        public Tweet[] GetTweetReplies(int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null)
            {
                return currentTweet.Replies.ToArray();
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public Tweet[] GetTweetReposts(int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null)
            {
                return currentTweet.Reposts.ToArray();
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public User[] GetTweetMentions(int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null)
            {
                return currentTweet.Mentions.ToArray();
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        //POST

        public Tweet PostTweet(string content, Credential credentials)
        {
            if (vs.ValidateUsernameExists(credentials.Username) && us.GetUserByUsername(credentials.Username).Credentials == credentials)
            {
                SimpleTweet tweet = new SimpleTweet(us.GetUserByUsername(credentials.Username), content);
                us.GetUserByUsername(credentials.Username).Authored.Add(tweet);
                db.SaveChanges();

                return tweet;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public void PostLike(Credential credentials, int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null || us.GetUserByUsername(credentials.Username).Credentials == credentials)
            {
                currentTweet.Likes.Add(us.GetUserByUsername(credentials.Username));
                us.GetUserByUsername(credentials.Username).Liked.Add(currentTweet);

                db.SaveChanges();
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public Tweet PostReply(string content, Credential credentials, int id)
        {
            if (vs.ValidateUsernameExists(credentials.Username) && us.GetUserByUsername(credentials.Username).Credentials == credentials)
            {
                var inReplyTo = GetTweetById(id);
                Reply tweet = new Reply(us.GetUserByUsername(credentials.Username), content, inReplyTo);
                inReplyTo.Replies.Add(tweet);
                db.SaveChanges();

                return tweet;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public Tweet PostRepost(Credential credentials, int id)
        {
            if (vs.ValidateUsernameExists(credentials.Username) && us.GetUserByUsername(credentials.Username).Credentials == credentials)
            {
                var repostOf = GetTweetById(id);
                Repost tweet = new Repost(us.GetUserByUsername(credentials.Username), repostOf);
                repostOf.Reposts.Add(tweet);
                db.SaveChanges();

                return tweet;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        //DELETE

        public Tweet DeleteTweetById(Credential credentials, int id)
        {
            var currentTweet = GetTweetById(id);

            if (!currentTweet.IsDeleted || currentTweet != null || us.GetUserByUsername(credentials.Username).Credentials == credentials)
            {
                var beforeDelete = GetTweetById(id);
                currentTweet.IsDeleted = true;
                db.SaveChanges();

                return beforeDelete;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}