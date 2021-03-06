using Application.DTO;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetOrdersQuery : IQuery<OrderSearchDto, PagedResponse<OrderDto>>
    {
    }
}
