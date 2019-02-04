using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAssessment.Models
{
    public abstract class Tweet
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Posted { get; }
        public string Content { get; set; }
        public List<HashTag> Tags { get; set; }
        public Tweet RepostOf { get; set; }
        public Tweet InReplyTo { get; set; }
        public bool IsDeleted { get; set; }
        public List<User> Mentions { get; set; }
        public List<User> Likes { get; set; }
        public List<Tweet> Replies { get; set; }
        public List<Tweet> Reposts { get; set; }
    }
}