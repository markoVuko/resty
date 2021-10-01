using Application.DTO;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetRecordTypesQuery : IQuery<RecordTypeSearchDto,PagedResponse<RecordTypeDto>>
    {
    }
}
