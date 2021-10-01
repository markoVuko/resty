using Application;
using DataAccess;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Loggers
{
    public class Logger : IUseCaseLogger
    {
        private readonly RadContext _con;

        public Logger(RadContext con)
        {
            _con = con;
        }
        public void Log(IAppActor actor, IUseCase uC, object req)
        {
            _con.ActionLogs.Add(new ActionLog
            {
                ActorId = actor.Id,
                ActionName = uC.Name,
                Description = JsonConvert.SerializeObject(req),
            });

            _con.SaveChanges();
        }
    }
}
