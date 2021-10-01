using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class ActionNotAuthorizedException : Exception
    {
        public ActionNotAuthorizedException(IUseCase uC, IAppActor actor)
            : base($"({DateTime.Now}) - User {actor.Identity} with an ID of {actor.Id} attempted unauthorized action: {uC.Name}")
        {

        }
    }
}
