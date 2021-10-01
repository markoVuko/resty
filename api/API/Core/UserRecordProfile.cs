using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class UserRecordProfile : Profile
    {
        public UserRecordProfile()
        {
            CreateMap<UserRecord, UserRecordDto>()
                .ForMember(
                dest => dest.RecordType,
                map => map.MapFrom(sors => sors.RecordType.Name))
                .ForMember(
                dest => dest.PayChange,
                map => map.MapFrom(sors => sors.RecordType.PayChange)); ;
            CreateMap<UserRecordDto, UserRecord>();
        }
    }
}
