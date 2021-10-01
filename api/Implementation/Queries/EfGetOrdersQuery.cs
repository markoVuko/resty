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
    public class EfGetOrdersQuery : IGetOrdersQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetOrdersQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 17;

        public string Name => "Get Orders Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public PagedResponse<OrderDto> Execute(OrderSearchDto req)
        {
            var q = _con.Orders.Include(x => x.User)
                .Include(x => x.OrderLines)
                .Where(x => x.IsDeleted == false)
                .AsQueryable();
            if (req.TablesFrom > 0)
            {
                q = q.Where(x => x.TableNumber >= req.TablesFrom);
            }
            if (req.TablesTo > 0)
            {
                q = q.Where(x => x.TableNumber <= req.TablesTo);
            }
            if (req.UserId > 0)
            {
                q = q.Where(x => x.UserId == req.UserId);
            }
            if (req.TotalPriceFrom>0)
            {
                q = q.Where(x => x.OrderLines.Sum(o => o.Price) >= req.TotalPriceFrom);
            }
            if (req.TotalPriceTo > 0)
            {
                q = q.Where(x => x.OrderLines.Sum(o => o.Price) <= req.TotalPriceTo);
            }

            if (req.Page == -1)
            {
                req.Page = 1;
                req.PerPage = q.Count();
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<OrderDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<OrderDto>(x)).ToList()
            };

            return res;
        }
    }
}
