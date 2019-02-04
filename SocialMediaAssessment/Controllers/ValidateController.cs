using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialMediaAssessment.Services;
using SocialMediaAssessment.DTOs;

namespace SocialMediaAssessment.Controllers
{
    public class ValidateController : ApiController
    {
        ValidateService service = new ValidateService();

        [Route("api/validate/tag/exists/{label}")]
        [HttpGet]
        public bool ValidateTagExists(string label)
        {
            return service.ValidateTagExists(label);
        }

        [Route("api/validate/username/exists/{username}")]
        [HttpGet]
        public bool ValidateUsernameExists(string username)
        {
            return service.ValidateUsernameExists(username);
        }

        [Route("api/validate/tag/exists/{username}")]
        [HttpGet]
        public bool ValidateUsernameAvailable(string username)
        {
            return service.ValidateUsernameAvailable(username);
        }
    }
}
