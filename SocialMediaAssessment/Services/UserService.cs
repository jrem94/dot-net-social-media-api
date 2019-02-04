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

        //GET

        //Retrieves all active, non-deleted, users as an array.
        public User[] GetUsers()
        {
            var checkUsers = from user in db.Users
                        where user.IsDeleted == false
                        select user;

            User[] users = checkUsers.ToArray();

            return users;

        }

        //Retrieves a user with the given username. If no such user exists or is deleted,
        //an error should be sent in lieu of a response.
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

        //Retrieves all (non-deleted) tweets authored by the user with the given username,
        //as well as all (non-deleted) tweets authored by users the given user is following.
        //This includes simple tweets, reposts, and replies. The tweets should appear in
        //reverse-chronological order. If no active user with that username exists
        //(deleted or never created), an error should be sent in lieu of a response.
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

        //Retrieves all (non-deleted) tweets authored by the user with the given username.
        //This includes simple tweets, reposts, and replies. The tweets should appear in
        //reverse-chronological order. If no active user with that username exists
        //(deleted or never created), an error should be sent in lieu of a response.
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

        //Retrieves all (non-deleted) tweets in which the user with the given username is
        //mentioned. The tweets should appear in reverse-chronological order. If no active
        //user with that username exists, an error should be sent in lieu of a response.
        //A user is considered “mentioned” by a tweet if the tweet has content and the user’s
        //username appears in that content following a @.
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

        //Retrieves the followers of the user with the given username. Only active users should
        //be included in the response. If no active user with the given username exists, an error
        //should be sent in lieu of a response.
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

        //Retrieves the users followed by the user with the given username. Only active users
        //should be included in the response. If no active user with the given username exists,
        //an error should be sent in lieu of a response.
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

        //POST

        //Creates a new user. If any required fields are missing or the username provided is
        //already taken, an error should be sent in lieu of a response. If the given credentials
        //match a previously-deleted user, re-activate the deleted user instead of creating a new one.
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

        //Subscribes the user whose credentials are provided by the request body to the user whose
        //username is given in the url. If there is already a following relationship between the two
        //users, no such followable user exists (deleted or never created), or the credentials provided
        //do not match an active user in the database, an error should be sent as a response.
        //If successful, no data is sent.
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

        //Unsubscribes the user whose credentials are provided by the request body from the user whose
        //username is given in the url. If there is no preexisting following relationship between the two
        //users, no such followable user exists (deleted or never created), or the credentials provided do
        //not match an active user in the database, an error should be sent as a response. If successful, no
        //data is sent.
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

        //PATCH

        //Updates the profile of a user with the given username. If no such user exists, the user is deleted,
        //or the provided credentials do not match the user, an error should be sent in lieu of a response. In
        //the case of a successful update, the returned user should contain the updated data.
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

        //DELETE

        //“Deletes” a user with the given username. If no such user exists or the provided credentials do not match
        //the user, an error should be sent in lieu of a response. If a user is successfully “deleted”, the response
        //should contain the user data prior to deletion.
        //IMPORTANT: This action should not actually drop any records from the database! Instead, develop a way to keep
        //track of “deleted” users so that if a user is re-activated, all of their tweets and information are restored.
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