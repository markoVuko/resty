using Application.DTO;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetSupplierQuery : IGetSupplierQuery
    {

        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetSupplierQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 40;

        public string Name => "Get Supplier Query";

        public List<int> AllowedRoles => new List<int> { 1 };

        public SupplierDto Execute(int req)
        {
            var r = _con.Suppliers.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(Supplier));
            }

            return _mapper.Map<SupplierDto>(r);
        }
    }
}
