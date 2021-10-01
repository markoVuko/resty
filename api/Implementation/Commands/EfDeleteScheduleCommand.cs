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
    public class EfDeleteScheduleCommand : IDeleteScheduleCommand
    {
        private readonly RadContext _con;
        public EfDeleteScheduleCommand(RadContext con)
        {
            _con = con;
        }
        public int Id => 4;

        public string Name => "Delete Schedule Command";

        public List<int> AllowedRoles => new List<int> { 1 };

        public void Execute(int req)
        {
            var s = _con.Schedules.Where(x => x.IsDeleted == false && x.Id == req).FirstOrDefault();
            if (s == null)
            {
                throw new InvalidEntityException(req, typeof(Schedule));
            }

            s.IsDeleted = true;
            s.IsActive = false;
            s.ModifiedAt = DateTime.UtcNow;
            _con.SaveChanges();
        }
    }
}
