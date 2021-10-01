using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(
                dest => dest.EmployeeFullName,
                map => map.MapFrom(sors => sors.User.FirstName + " " + sors.User.LastName));
            CreateMap<OrderDto, Order>();
        }
    }
}
