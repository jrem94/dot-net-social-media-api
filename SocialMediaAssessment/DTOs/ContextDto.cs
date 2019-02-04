using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;

namespace SocialMediaAssessment.DTOs
{
    public class ContextDto
    {
        public Tweet Target { get; set; }
        public List<Tweet> Before { get; set; }
        public List<Tweet> After { get; set; }
    }
}