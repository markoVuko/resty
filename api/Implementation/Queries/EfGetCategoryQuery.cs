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
    public class EfGetCategoryQuery : IGetCategoryQuery
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfGetCategoryQuery(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 57;

        public string Name => "Get Category Query";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public CategoryDto Execute(int req)
        {
            var r = _con.Categories.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(Category));
            }

            return _mapper.Map<CategoryDto>(r);
        }
    }
}
