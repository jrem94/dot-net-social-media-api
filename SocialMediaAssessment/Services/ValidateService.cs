using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;
using SocialMediaAssessment.DTOs;

namespace SocialMediaAssessment.Services
{
    public class ValidateService
    {
        ApiContext db = new ApiContext();

        public bool ValidateTagExists(string label)
        {
            var checkTag = from tag in db.HashTags
                            where tag.Label == label
                            select tag;

            if(checkTag != null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool ValidateUsernameExists(string username)
        {
            var checkUsername = from name in db.Users
                                where name.Username == username
                                select name;

            if (checkUsername != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }

        public bool ValidateUsernameAvailable(string username)
        {
            if (ValidateUsernameExists(username))
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }

    }
}