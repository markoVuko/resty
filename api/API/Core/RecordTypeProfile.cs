using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class RecordTypeProfile : Profile
    {
        public RecordTypeProfile()
        {
            CreateMap<RecordType, RecordTypeDto>();
            CreateMap<RecordTypeDto, RecordType>();
        }
    }
}
