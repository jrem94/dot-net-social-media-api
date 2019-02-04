using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialMediaAssessment.Services;
using SocialMediaAssessment.Models;

namespace SocialMediaAssessment.Controllers
{
    public class UserController : ApiController
    {
        UserService service = new UserService();
        

        //GET

        [Route("api/users")]
        [HttpGet]
        public User[] GetUsers()
        {
            return service.GetUsers();
        }

        [Route("api/users/{username: string}")]
        [HttpGet]
        public User GetUserByUsername(string username)
        {
            return service.GetUserByUsername(username);
        }

        [Route("api/users/{username: string}/feed")]
        [HttpGet]
        public Tweet[] GetFeedByUsername(string username)
        {
            return service.GetFeedByUsername(username);
        }

        [Route("api/users/{username: string}/tweets")]
        [HttpGet]
        public Tweet[] GetTweetsByUsername(string username)
        {
            return service.GetTweetsByUsername(username);
        }

        [Route("api/users/{username: string}/mentions")]
        [HttpGet]
        public Tweet[] GetMentionsByUsername(string username)
        {
            return service.GetMentionsByUsername(username);
        }

        [Route("api/users/{username: string}/followers")]
        [HttpGet]
        public User[] GetFollowersByUsername(string username)
        {
            return service.GetFollowersByUsername(username);
        }

        [Route("api/users/{username: string}/following")]
        [HttpGet]
        public User[] GetFollowingByUsername(string username)
        {
            return service.GetFollowingByUsername(username);
        }

        //POST

        [Route("api/users")]
        [HttpPost]
        public User PostUser([FromBody]Credential credentials, Profile profile)
        {
            return service.PostUser(credentials, profile);
        }

        [Route("api/users/{username: string}/follow")]
        [HttpPost]
        public void SubscribeUser([FromBody]Credential credentials, string username)
        {
            service.SubscribeUser(credentials, username);
        }

        [Route("api/users/{username: string}/unfollow")]
        [HttpPost]
        public void UnsubscribeUser([FromBody] Credential credentials, string username)
        {
            service.UnsubscribeUser(credentials, username);
        }

        //PATCH

        [Route("api/users/{username: string}")]
        [HttpPatch]
        public User UpdateUserProfile([FromBody]Credential credentials, Profile profile, string username)
        {
            return service.UpdateUserProfile(credentials, profile, username);
        }

        [Route("api/users/{username: string}")]
        [HttpDelete]
        public User DeleteUserByUsername([FromBody] Credential credentials, string username)
        {
            return service.DeleteUserByUsername(credentials, username);
        }
    }
}