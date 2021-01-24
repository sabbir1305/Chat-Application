using AutoMapper;
using ChatApi.Entities;
using ChatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();

            CreateMap<MessageModel, Message>();
            CreateMap<Message, MessageDisplayModel>();
        }
    }
}
