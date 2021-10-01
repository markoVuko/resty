using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<ActionLog, LogDto>();
            CreateMap<LogDto, ActionLog>();
        }
    }
}
