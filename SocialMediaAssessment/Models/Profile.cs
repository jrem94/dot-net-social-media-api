using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAssessment.Models
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public Profile() { }

        public Profile(string firstname, string lastname, string email, string phone)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Phone = phone;
        }
    }
}