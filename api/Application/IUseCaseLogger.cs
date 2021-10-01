using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IUseCaseLogger
    {
        void Log(IAppActor actor, IUseCase uC, object request);
    }
}
