using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        private readonly RadContext _con;
        public EfDeleteUserCommand(RadContext con)
        {
            _con = con;
        }
        public int Id => 26;

        public string Name => "Delete User Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(int req)
        {
            var r = _con.Users.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();

            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(User));
            }

            r.IsDeleted = true;
            r.ModifiedAt = DateTime.UtcNow;
            r.IsActive = false;

            _con.SaveChanges();
        }
    }
}
