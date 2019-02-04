using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialMediaAssessment.Services;
using SocialMediaAssessment.Models;
using SocialMediaAssessment.DTOs;
using AutoMapper;

namespace SocialMediaAssessment.Controllers
{
    public class UserController : ApiController
    {
        UserService service = new UserService();

        [Route("api/users")]
        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            return service.GetUsers().Select(Mapper.Map<User, UserDto>);
        }

        [Route("api/users/{username}")]
        [HttpGet]
        public UserDto GetUserByUsername(string username)
        {
            return Mapper.Map<User, UserDto>(service.GetUserByUsername(username));
        }

        [Route("api/users/{username}/feed")]
        [HttpGet]
        public IEnumerable<TweetDto> GetFeedByUsername(string username)
        {
            return service.GetFeedByUsername(username).Select(Mapper.Map<Tweet, TweetDto>);
        }

        [Route("api/users/{username}/tweets")]
        [HttpGet]
        public IEnumerable<TweetDto> GetTweetsByUsername(string username)
        {
            return service.GetTweetsByUsername(username).Select(Mapper.Map<Tweet, TweetDto>);
        }

        [Route("api/users/{username}/mentions")]
        [HttpGet]
        public IEnumerable<TweetDto> GetMentionsByUsername(string username)
        {
            return service.GetMentionsByUsername(username).Select(Mapper.Map<Tweet, TweetDto>);
        }

        [Route("api/users/{username}/followers")]
        [HttpGet]
        public IEnumerable<UserDto> GetFollowersByUsername(string username)
        {
            return service.GetFollowersByUsername(username).Select(Mapper.Map<User, UserDto>);
        }

        [Route("api/users/{username}/following")]
        [HttpGet]
        public IEnumerable<UserDto> GetFollowingByUsername(string username)
        {
            return service.GetFollowingByUsername(username).Select(Mapper.Map<User, UserDto>);
        }

        [Route("api/users")]
        [HttpPost]
        public UserDto PostUser([FromBody]Credential credentials, Models.Profile profile)
        {
            return Mapper.Map < User, UserDto > (service.PostUser(credentials, profile));
        }

        [Route("api/users/{username}/follow")]
        [HttpPost]
        public void SubscribeUser([FromBody]Credential credentials, string username)
        {
            service.SubscribeUser(credentials, username);
        }

        [Route("api/users/{username}/unfollow")]
        [HttpPost]
        public void UnsubscribeUser([FromBody] Credential credentials, string username)
        {
            service.UnsubscribeUser(credentials, username);
        }

        [Route("api/users/{username}")]
        [HttpPatch]
        public UserDto UpdateUserProfile([FromBody]Credential credentials, Models.Profile profile, string username)
        {
            return Mapper.Map < User, UserDto > (service.UpdateUserProfile(credentials, profile, username));
        }

        [Route("api/users/{username}")]
        [HttpDelete]
        public UserDto DeleteUserByUsername([FromBody] Credential credentials, string username)
        {
            return Mapper.Map < User, UserDto > (service.DeleteUserByUsername(credentials, username));
        }
    }
}