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
    public class TagController : ApiController
    {
        TagService service = new TagService();

        [Route("api/tags")]
        [HttpGet]
        public IEnumerable<HashTagDto> GetTags()
        {
            return service.GetTags().Select(Mapper.Map<HashTag, HashTagDto>);
        }

        [Route("api/tags/{label}")]
        [HttpGet]
        public IEnumerable<TweetDto> GetTagsByLabel(string label)
        {
            return service.GetTagsByLabel(label).Select(Mapper.Map<Tweet, TweetDto>);
        }
    }
}