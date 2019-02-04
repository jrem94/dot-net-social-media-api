using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAssessment.Models
{
    public class HashTag
    {
        public string Label { get; set; }
        public DateTime FirstUsed { get; }
        public DateTime LastUsed { get; set; }
        public List<Tweet> Tweets { get; set; }

        public HashTag() { }

        public HashTag(string label)
        {
            this.Label = label;
            this.FirstUsed = DateTime.Now;
        }
    }
}