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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkTypesController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public WorkTypesController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/WorkTypes
        [HttpGet]
        public IActionResult Get([FromQuery] WorkTypeSearchDto catSDto, [FromServices] IGetWorkTypesQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/WorkTypes/5
        [HttpGet("{id}", Name = "GetWorkType")]
        public IActionResult Get(int id, [FromServices] IGetWorkTypeQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/WorkTypes
        [HttpPost]
        public void Post([FromBody] WorkTypeDto dto, [FromServices] ICreateWorkTypeCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/WorkTypes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] WorkTypeDto dto, [FromServices] IEditWorkTypeComand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/WorkTypes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteWorkTypeCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }

    }
}
