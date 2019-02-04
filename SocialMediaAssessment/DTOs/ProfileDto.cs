using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAssessment.DTOs
{
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}