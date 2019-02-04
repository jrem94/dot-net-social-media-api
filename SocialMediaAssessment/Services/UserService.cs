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
    public class UserService
    {
        ApiContext db = new ApiContext();
        ValidateService vs = new ValidateService();

        public User[] GetUsers()
        {
            var checkUsers = from user in db.Users
                        where user.IsDeleted == false
                        select user;

            User[] users = checkUsers.ToArray();

            return users;

        }

        public User GetUserByUsername(string username)
        {
            var users = GetUsers();

            foreach(User user in users)
            {
                if (user.Username == username && user.IsDeleted == false)
                {
                    return user;
                }
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public Tweet[] GetFeedByUsername(string username)
        {
            var currentUser = GetUserByUsername(username);

            List<Tweet> feed = new List<Tweet>();

            foreach(User following in currentUser.Following)
            {
                var list = following.Authored.ToArray();
                foreach(Tweet tweet in list)
                {
                    if (!tweet.IsDeleted)
                    {
                        feed.Add(tweet);
                    }
                    
                }
            }

            foreach (Tweet tweet in currentUser.Authored)
            {
                var list = currentUser.Authored.ToArray();
                foreach (Tweet element in list)
                {
                    if (!element.IsDeleted)
                    {
                        feed.Add(element);
                    }
                    
                }
            }

            Tweet[] userFeed = feed.ToArray();

            return userFeed;
        }

        public Tweet[] GetTweetsByUsername(string username)
        {
            var x = from tweet in db.SimpleTweets
                    where tweet.User.Username == username
                    select tweet;

            var y = from tweet in db.Reposts
                    where tweet.User.Username == username
                    select tweet;

            var z = from tweet in db.Replies
                    where tweet.User.Username == username
                    select tweet;

            Tweet[] xtweets = x.ToArray<Tweet>();
            Tweet[] ytweets = y.ToArray<Tweet>();
            Tweet[] ztweets = z.ToArray<Tweet>();

            Tweet[] allTweets = new Tweet[(xtweets.Length + ytweets.Length + ztweets.Length)];

            Array.Copy(xtweets, allTweets, xtweets.Length);
            Array.Copy(ytweets, allTweets, ytweets.Length);
            Array.Copy(ztweets, allTweets, ztweets.Length);

            List<Tweet> userTweets = new List<Tweet>();

            foreach (Tweet tweet in allTweets)
            {
                if (!tweet.IsDeleted)
                {
                    userTweets.Add(tweet);
                }
            }

            Tweet[] nondeletedTweets = userTweets.ToArray();

            return nondeletedTweets;
        }

        public Tweet[] GetMentionsByUsername(string username)
        {
            var x = from tweet in db.SimpleTweets
                    where tweet.Mentions != null
                    select tweet;

            var y = from tweet in db.Reposts
                    where tweet.Mentions != null
                    select tweet;

            var z = from tweet in db.Replies
                    where tweet.Mentions != null
                    select tweet;

            Tweet[] xtweets = x.ToArray<Tweet>();
            Tweet[] ytweets = y.ToArray<Tweet>();
            Tweet[] ztweets = z.ToArray<Tweet>();

            Tweet[] allTweets = new Tweet[(xtweets.Length + ytweets.Length + ztweets.Length)];

            Array.Copy(xtweets, allTweets, xtweets.Length);
            Array.Copy(ytweets, allTweets, ytweets.Length);
            Array.Copy(ztweets, allTweets, ztweets.Length);

            List<Tweet> mentionedTweets = new List<Tweet>();

            foreach (Tweet tweet in allTweets)
            {
                if (!tweet.IsDeleted)
                {
                    foreach(User mention in tweet.Mentions)
                    {
                        if ($"{mention}" == username && !mention.IsDeleted && vs.ValidateUsernameExists(username))
                        {
                            mentionedTweets.Add(tweet);
                        }
                    }
                }
            }

            Tweet[] nondeletedTweets = mentionedTweets.ToArray();

            return nondeletedTweets;
        }

        public User[] GetFollowersByUsername(string username)
        {
            var currentUser = GetUserByUsername(username);

            List<User> followers = new List<User>();

            foreach (User follower in currentUser.Followers)
            {
                    if (!follower.IsDeleted)
                    {
                        followers.Add(follower);
                    }
            }

            User[] usersFollowers = followers.ToArray();

            return usersFollowers;
        }

        public User[] GetFollowingByUsername(string username)
        {
            var currentUser = GetUserByUsername(username);

            List<User> following = new List<User>();

            foreach (User follow in currentUser.Following)
            {
                if (!follow.IsDeleted)
                {
                    following.Add(follow);
                }
            }

            User[] usersFollowing = following.ToArray();

            return usersFollowing;
        }

        public User PostUser(Credential credentials, Profile profile)
        {
            if (vs.ValidateUsernameExists(credentials.Username) && GetUserByUsername(credentials.Username).Profile.Email == profile.Email &&
                GetUserByUsername(credentials.Username).Credentials.Password == credentials.Password)
            {
                var reactivate = GetUserByUsername(credentials.Username);
                reactivate.IsDeleted = false;

                db.SaveChanges();

                return reactivate;
            }
            else if (profile.Email != null || vs.ValidateUsernameAvailable(credentials.Username) || credentials.Username != null ||
                credentials.Password != null)
            {
                User user = new User();
                user.Credentials = credentials;
                user.Profile = profile;

                db.SaveChanges();

                return user;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public void SubscribeUser(Credential credentials, string username)
        {
            var users = GetUsers().ToList();

            if (!vs.ValidateUsernameExists(username) || GetUserByUsername(username).IsDeleted || !users.Contains(GetUserByUsername(credentials.Username))
                || GetUserByUsername(credentials.Username).Following.Contains(GetUserByUsername(username)))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else if (!GetUserByUsername(credentials.Username).Following.Contains(GetUserByUsername(username)))
            {
                GetUserByUsername(credentials.Username).Following.Add(GetUserByUsername(username));
                GetUserByUsername(username).Followers.Add(GetUserByUsername(credentials.Username));

                db.SaveChanges();
            }
            
        }

        public void UnsubscribeUser(Credential credentials, string username)
        {
            var users = GetUsers().ToList();

            if (!vs.ValidateUsernameExists(username) || GetUserByUsername(username).IsDeleted || !users.Contains(GetUserByUsername(credentials.Username))
                || !GetUserByUsername(credentials.Username).Following.Contains(GetUserByUsername(username)))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else if (GetUserByUsername(credentials.Username).Following.Contains(GetUserByUsername(username)))
            {
                GetUserByUsername(credentials.Username).Following.Remove(GetUserByUsername(username));
                GetUserByUsername(username).Followers.Remove(GetUserByUsername(credentials.Username));

                db.SaveChanges();
            }
        }

        public User UpdateUserProfile(Credential credentials, Profile profile, string username)
        {
            if (vs.ValidateUsernameExists(credentials.Username) && GetUserByUsername(credentials.Username).Credentials == credentials &&
                !GetUserByUsername(credentials.Username).IsDeleted)
            {
                var user = GetUserByUsername(username);
                user.Profile = profile;
                db.SaveChanges();
                return user;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public User DeleteUserByUsername(Credential credentials, string username)
        {
            if (vs.ValidateUsernameExists(username) && credentials == GetUserByUsername(username).Credentials)
            {
                var user = GetUserByUsername(username);

                GetUserByUsername(username).IsDeleted = true;
                db.SaveChanges();

                return user;
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}