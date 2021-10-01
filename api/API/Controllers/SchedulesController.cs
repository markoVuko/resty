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
    public class SchedulesController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public SchedulesController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/Schedules
        [HttpGet]
        public IActionResult Get([FromQuery] ScheduleSearchDto catSDto, [FromServices] IGetSchedulesQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/Schedules/5
        [HttpGet("{id}", Name = "GetSchedule")]
        public IActionResult Get(int id, [FromServices] IGetScheduleQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/Schedules
        [HttpPost]
        public void Post([FromBody] CreateScheduleDto dto, [FromServices] ICreateScheduleCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CreateScheduleDto dto, [FromServices] IEditSetScheduleCommand comm)
        {
            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteScheduleCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }
    }
}
