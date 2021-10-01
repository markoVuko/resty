using Application.DTO;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetSuppliersQuery : IGetSuppliersQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetSuppliersQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 37;

        public string Name => "Get Suppliers Query";

        public List<int> AllowedRoles => new List<int> { 1 };

        public PagedResponse<SupplierDto> Execute(SupplierSearchDto req)
        {
            var q = _con.Suppliers.Where(x => x.IsDeleted == false).AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Keyword))
            {
                q = q.Where(x => x.Name.ToLower().Contains(req.Keyword.ToLower())
                || x.Address.ToLower().Contains(req.Keyword.ToLower())
                || x.City.ToLower().Contains(req.Keyword.ToLower())
                || x.Mail.ToLower().Contains(req.Keyword.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(req.Phone))
            {
                q = q.Where(x => x.Phone.ToLower().Contains(req.Phone.ToLower()));
            }

            if (req.Page == -1)
            {
                req.Page = 1;
                req.PerPage = q.Count();
            }

            var offset = req.PerPage * (req.Page - 1);

            var res = new PagedResponse<SupplierDto>
            {
                PerPage = req.PerPage,
                TotalItems = q.Count(),
                CurrentPage = req.Page,
                Items = q.Skip(offset).Take(req.PerPage).Select(x => _mapper.Map<SupplierDto>(x)).ToList()
            };

            return res;
        }
    }
}
