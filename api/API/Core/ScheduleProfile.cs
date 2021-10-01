using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, CreateScheduleDto>()
                .ForMember(
                dest => dest.WorkType,
                map => map.MapFrom(sors => sors.WorkType.Name))
                .ForMember(
                dest => dest.HourlyRate,
                map => map.MapFrom(sors => sors.WorkType.HourlyRate));
            CreateMap<CreateScheduleDto, Schedule>();
        }
    }
}
