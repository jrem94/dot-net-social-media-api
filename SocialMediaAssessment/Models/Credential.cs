using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAssessment.Models
{
    public class Credential
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Credential() { }

        public Credential(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}