using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialMediaAssessment.Models;
using SocialMediaAssessment.DTOs;
using AutoMapper;

namespace SocialMediaAssessment.App_Start
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Models.Profile, ProfileDto>();
            CreateMap<Credential, CredentialDto>();
            CreateMap<Context, ContextDto>();
            CreateMap<HashTag, HashTagDto>();
            CreateMap<Reply, ReplyDto>();
            CreateMap<Repost, RepostDto>();
            CreateMap<SimpleTweet, SimpleTweetDto>();
        }
    }
}

