using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteItemCommand : IDeleteItemCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        public EfDeleteItemCommand(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }
        public int Id => 9;

        public string Name => "Delete Item Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(int req)
        {
            var item = _con.Items.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (item == null)
            {
                throw new InvalidEntityException(req, typeof(Item));
            }

            item.IsDeleted = true;
            item.IsActive = false;
            item.ModifiedAt = DateTime.UtcNow;

            _con.SaveChanges();

        }
    }
}
