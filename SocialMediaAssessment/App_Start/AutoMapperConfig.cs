using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SocialMediaAssessment.Models;
using SocialMediaAssessment.DTOs;

namespace SocialMediaAssessment.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Models.Profile, ProfileDto>();
                cfg.CreateMap<Credential, CredentialDto>();
                cfg.CreateMap<Context, ContextDto>();
                cfg.CreateMap<HashTag, HashTagDto>();
                cfg.CreateMap<Reply, ReplyDto>();
                cfg.CreateMap<Repost, RepostDto>();
                cfg.CreateMap<SimpleTweet, SimpleTweetDto>();
            });
        }
    }
}