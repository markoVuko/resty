using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public OrdersController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/Orders
        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearchDto catSDto, [FromServices] IGetOrdersQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id, [FromServices] IGetOrderQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/Orders
        [HttpPost]
        public void Post([FromBody] OrderDto dto, [FromServices] ICreateOrderCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OrderDto dto, [FromServices] IEditOrderCommand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteOrderCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }
    }
}
