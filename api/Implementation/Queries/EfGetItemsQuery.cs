using Application.DTO;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetItemsQuery : IGetItemsQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetItemsQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 12;

        public string Name => "Get Items Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public PagedResponse<ItemDto> Execute(ItemSearchDto req)
        {
            var q = _con.Items.Include(x => x.Supplier)
                .Include(x => x.CategoryItems)
                .ThenInclude(x => x.Category)
                .Where(x => x.IsDeleted == false)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                q = q.Where(x => x.Name.ToLower().Contains(req.Name.ToLower()));
            }
            if (req.SupplierId > 0)
            {
                q = q.Where(x => x.Supplier.Id == req.SupplierId);
            }
            if (req.CategoryId > 0)
            {
                q = q.Where(x => x.CategoryItems.Any(c => c.Category.Id == req.CategoryId));
            }
            if (req.MinPrice > 0)
            {
                q = q.Where(x => x.Price >= req.MinPrice);
            }
            if (req.MaxPrice > 0)
            {
                q = q.Where(x => x.Price <= req.MaxPrice);
            }
            if(req.Below == 1)
            {
                q = q.Where(x => x.Quantity <= x.MinQuantity);
            }

            if (req.Page == -1)
            {
                req.Page = 1;
                req.PerPage = q.Count();
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<ItemDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<ItemDto>(x)).ToList()
            };

            return res;
        }
    }
}
