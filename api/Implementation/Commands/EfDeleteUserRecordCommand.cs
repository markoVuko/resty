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
    public class EfDeleteUserRecordCommand : IDeleteUserRecordCommand
    {
        private readonly RadContext _con;
        public EfDeleteUserRecordCommand(RadContext con)
        {
            _con = con;
        }
        public int Id => 31;

        public string Name => "Delete User Record Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(int req)
        {
            var ur = _con.UserRecords
                .Where(x => x.IsDeleted == false && x.Id == req)
                .FirstOrDefault();
            if (ur == null)
            {
                throw new InvalidEntityException(req, typeof(UserRecord));
            }

            ur.IsDeleted = true;
            ur.ModifiedAt = DateTime.UtcNow;
            ur.IsActive = false;

            _con.SaveChanges();
        }
    }
}
