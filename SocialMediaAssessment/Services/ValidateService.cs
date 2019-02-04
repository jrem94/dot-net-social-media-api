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

        //Checks whether or not a given hashtag exists.
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

        //Checks whether or not a given username exists.
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

        //Checks whether or not a given username is available.
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