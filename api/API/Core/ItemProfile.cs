using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(
                dest => dest.Categories,
                map => map.MapFrom(sors => sors.CategoryItems.Select(x=>x.Category).ToList()))
                .ForMember(
                dest => dest.Supplier,
                map => map.MapFrom(sors => sors.Supplier.Name));
            CreateMap<ItemDto, Item>();
        }
    }
}
