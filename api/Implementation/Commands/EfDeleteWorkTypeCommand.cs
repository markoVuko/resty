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
    public class EfDeleteWorkTypeCommand : IDeleteWorkTypeCommand
    {
        private readonly RadContext _con;
        public EfDeleteWorkTypeCommand(RadContext con)
        {
            _con = con;
        }
        public int Id => 61;

        public string Name => "Delete Work Type Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(int req)
        {
            var r = _con.WorkTypes.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (r == null)
            {
                throw new InvalidEntityException(req, typeof(WorkType));
            }

            r.IsDeleted = true;
            r.ModifiedAt = DateTime.UtcNow;
            r.IsActive = false;

            _con.SaveChanges();
        }
    }
}
