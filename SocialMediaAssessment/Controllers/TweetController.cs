using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialMediaAssessment.Services;
using SocialMediaAssessment.Models;
using SocialMediaAssessment.DTOs;

namespace SocialMediaAssessment.Controllers
{
    public class TweetController : ApiController
    {
        TweetService service = new TweetService();

        //GET

        [Route("api/tweets")]
        [HttpGet]
        public Tweet[] GetTweets()
        {
            return service.GetTweets();
        }

        [Route("api/tweets/{id}")]
        [HttpGet]
        public Tweet GetTweetById(int id)
        {
            return service.GetTweetById(id);
        }

        [Route("api/tweets/{id}/tags")]
        [HttpGet]
        public HashTag[] GetTweetTags(int id)
        {
            return service.GetTweetTags(id);
        }

        [Route("api/tweets/{id}/likes")]
        [HttpGet]
        public User[] GetTweetLikes(int id)
        {
            return service.GetTweetLikes(id);
        }

        [Route("api/tweets/{id}/context")]
        [HttpGet]
        public Context GetTweetContext(int id)
        {
            return service.GetTweetContext(id);
        }

        [Route("api/tweets/{id}/replies")]
        [HttpGet]
        public Tweet[] GetTweetReplies(int id)
        {
            return service.GetTweetReplies(id);
        }

        [Route("api/tweets/{id}/reposts")]
        [HttpGet]
        public Tweet[] GetTweetReposts(int id)
        {
            return service.GetTweetReposts(id);
        }

        [Route("api/tweets/{id}/mentions")]
        [HttpGet]
        public User[] GetTweetMentions(int id)
        {
            return service.GetTweetMentions(id);
        }

        //POST

        [Route("api/tweets")]
        [HttpPost]
        public Tweet PostTweet([FromBody]string content, Credential credentials)
        {
            return service.PostTweet(content, credentials);
        }

        [Route("api/tweets/{id}/like")]
        [HttpPost]
        public void PostLike([FromBody]Credential credentials, int id)
        {
            service.PostLike(credentials, id);
        }

        [Route("api/tweets/{id}/reply")]
        [HttpPost]
        public Tweet PostReply([FromBody]string content, Credential credentials, int id)
        {
            return service.PostReply(content, credentials, id);
        }

        [Route("api/tweets/{id}/repost")]
        [HttpPost]
        public Tweet PostRepost([FromBody]Credential credentials, int id)
        {
            return service.PostRepost(credentials, id);
        }

        //DELETE

        [Route("api/tweets/{id}")]
        [HttpDelete]
        public Tweet DeleteTweetById([FromBody] Credential credentials, int id)
        {
            return service.DeleteTweetById(credentials, id);
        }
    }
}