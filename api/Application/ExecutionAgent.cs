using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class ExecutionAgent
    {
        private readonly IAppActor actor;
        private readonly IUseCaseLogger logger;
        public ExecutionAgent(IAppActor actor, IUseCaseLogger logger)
        {
            this.actor = actor;
            this.logger = logger;
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> comm, TRequest req)
        {
            logger.Log(actor, comm, req);
            if (!comm.AllowedRoles.Contains(actor.RoleId))
            {
                throw new ActionNotAuthorizedException(comm, actor);
            }
            comm.Execute(req);
        }

        public TResponse ExecuteQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest req)
        {
            logger.Log(actor, query, req);
            if (!query.AllowedRoles.Contains(actor.RoleId))
            {
                throw new ActionNotAuthorizedException(query, actor);
            }
            return query.Execute(req);
        }
    }
}
