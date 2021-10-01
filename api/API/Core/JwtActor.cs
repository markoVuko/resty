using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class JwtActor : IAppActor
    {
        public int Id { get; set; }

        public string Identity { get; set; }

        public int RoleId { get; set; }

    }
}
