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
    public class TagController : ApiController
    {
        TagService service = new TagService();

        [Route("api/tags")]
        [HttpGet]
        public HashTag[] GetTags()
        {
            return service.GetTags();
        }

        [Route("api/tags/{label}")]
        [HttpGet]
        public Tweet[] GetTagsByLabel(string label)
        {
            return service.GetTagsByLabel(label);
        }
    }
}