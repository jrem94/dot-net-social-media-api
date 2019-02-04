using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;

namespace SocialMediaAssessment.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public Profile Profile { get; set; }
        public DateTime Joined { get; }
    }
}