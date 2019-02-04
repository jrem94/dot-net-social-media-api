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
    public class TweetController : ApiController
    {
        TweetService service = new TweetService();

        [Route("api/tweets")]
        [HttpGet]
        public IEnumerable<TweetDto> GetTweets()
        {
            return service.GetTweets().Select(Mapper.Map<Tweet, TweetDto>);
        }

        [Route("api/tweets/{id}")]
        [HttpGet]
        public TweetDto GetTweetById(int id)
        {
            return Mapper.Map<Tweet, TweetDto>(service.GetTweetById(id));
        }

        [Route("api/tweets/{id}/tags")]
        [HttpGet]
        public IEnumerable<HashTagDto> GetTweetTags(int id)
        {
            return service.GetTweetTags(id).Select(Mapper.Map<HashTag, HashTagDto>);
        }

        [Route("api/tweets/{id}/likes")]
        [HttpGet]
        public IEnumerable<UserDto> GetTweetLikes(int id)
        {
            return service.GetTweetLikes(id).Select(Mapper.Map<User, UserDto>);
        }

        [Route("api/tweets/{id}/context")]
        [HttpGet]
        public ContextDto GetTweetContext(int id)
        {
            return Mapper.Map<Context, ContextDto>(service.GetTweetContext(id));
        }

        [Route("api/tweets/{id}/replies")]
        [HttpGet]
        public IEnumerable<TweetDto> GetTweetReplies(int id)
        {
            return service.GetTweetReplies(id).Select(Mapper.Map<Tweet, TweetDto>);
        }

        [Route("api/tweets/{id}/reposts")]
        [HttpGet]
        public IEnumerable<TweetDto> GetTweetReposts(int id)
        {
            return service.GetTweetReposts(id).Select(Mapper.Map<Tweet, TweetDto>);
        }

        [Route("api/tweets/{id}/mentions")]
        [HttpGet]
        public IEnumerable<UserDto> GetTweetMentions(int id)
        {
            return service.GetTweetMentions(id).Select(Mapper.Map<User, UserDto>);
        }

        //POST

        [Route("api/tweets")]
        [HttpPost]
        public TweetDto PostTweet([FromBody]string content, Credential credentials)
        {
            return Mapper.Map<Tweet, TweetDto>(service.PostTweet(content, credentials));
        }

        [Route("api/tweets/{id}/like")]
        [HttpPost]
        public void PostLike([FromBody]Credential credentials, int id)
        {
            service.PostLike(credentials, id);
        }

        [Route("api/tweets/{id}/reply")]
        [HttpPost]
        public TweetDto PostReply([FromBody]string content, Credential credentials, int id)
        {
            return Mapper.Map<Tweet, TweetDto>(service.PostReply(content, credentials, id));
        }

        [Route("api/tweets/{id}/repost")]
        [HttpPost]
        public TweetDto PostRepost([FromBody]Credential credentials, int id)
        {
            return Mapper.Map<Tweet, TweetDto>(service.PostRepost(credentials, id));
        }

        //DELETE

        [Route("api/tweets/{id}")]
        [HttpDelete]
        public TweetDto DeleteTweetById([FromBody] Credential credentials, int id)
        {
            return Mapper.Map<Tweet, TweetDto>(service.DeleteTweetById(credentials, id));
        }
    }
}