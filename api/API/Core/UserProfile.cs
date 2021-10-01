using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                dest => dest.Role,
                map => map.MapFrom(sors => sors.Role.Name));
            CreateMap<UserDto, User>();
        }
    }
}
