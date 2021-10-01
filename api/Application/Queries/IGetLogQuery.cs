﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetLogQuery : IQuery<int, LogDto>
    {
    }
}
