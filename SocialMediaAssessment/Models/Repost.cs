using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAssessment.Models
{
    public class Repost : Tweet
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public DateTime Posted { get; }
        public Tweet RepostOf { get; set; }
        public List<HashTag> Tags { get; set; }
        public bool IsDeleted { get; set; }
        public List<User> Mentions { get; set; }
        public List<User> Likes { get; set; }
        public List<Tweet> Replies { get; set; }
        public List<Tweet> Reposts { get; set; }

        public Repost() { }

        public Repost(User author, Tweet repostOf)
        {
            this.Author = author;
            this.RepostOf = repostOf;
            this.Posted = DateTime.Now;
        }
    }
}