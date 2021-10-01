using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class WorkTypeProfile : Profile
    {
        public WorkTypeProfile()
        {
            CreateMap<WorkType, WorkTypeDto>();
            CreateMap<WorkTypeDto, WorkType>();
        }
    }
}
