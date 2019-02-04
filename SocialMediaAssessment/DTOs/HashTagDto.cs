using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;

namespace SocialMediaAssessment.DTOs
{
    public class HashTagDto
    {
        public string Label { get; set; }
        public DateTime FirstUsed { get; }
        public DateTime LastUsed { get; set; }
    }
}