using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;

namespace SocialMediaAssessment.DTOs
{
    public class RepostDto : TweetDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Posted { get; }
        public Tweet RepostOf { get; set; }
    }
}