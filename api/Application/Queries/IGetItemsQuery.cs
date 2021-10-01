using Application.DTO;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetItemsQuery : IQuery<ItemSearchDto, PagedResponse<ItemDto>>
    {
    }
}
