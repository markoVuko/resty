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
    public class ItemsController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public ItemsController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/Items
        [HttpGet]
        public IActionResult Get([FromQuery] ItemSearchDto catSDto, [FromServices] IGetItemsQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/Items/5
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult Get(int id, [FromServices] IGetItemQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/Items
        [HttpPost]
        public void Post([FromBody] ItemDto dto, [FromServices] ICreateItemCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ItemDto dto, [FromServices] IEditItemCommand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteItemCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }
    }
}
