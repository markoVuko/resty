using Application.DTO;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetWorkTypesQuery : IQuery<WorkTypeSearchDto, PagedResponse<WorkTypeDto>>
    {
    }
}
