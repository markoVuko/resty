using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ActionLog : Entity
    {
        public int ActorId { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
    }
}
