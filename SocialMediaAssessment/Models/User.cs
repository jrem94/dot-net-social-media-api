using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAssessment.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Profile Profile { get; set; }
        public DateTime Joined { get; set; }
        public bool IsDeleted { get; set; }
        public List<User> Following { get; set; } //This user is following others
        public List<User> Followers { get; set; } //Others are following this user
        public List<Tweet> Authored { get; set; }
        public List<Tweet> Liked { get; set; }
        public Credential Credentials { get; set; }

        public User() { }

        public User(string username, Profile profile)
        {
            this.Username = username;
            this.Profile = profile;
            this.Joined = DateTime.Now;
        }
    }
}