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
    public class UserRecordsController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public UserRecordsController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/UserRecords
        [HttpGet]
        public IActionResult Get([FromQuery] UserRecordSearchDto catSDto, [FromServices] IGetUserRecordsQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/UserRecords/5
        [HttpGet("{id}", Name = "GetUserRecord")]
        public IActionResult Get(int id, [FromServices] IGetUserRecordQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/UserRecords
        [HttpPost]
        public void Post([FromBody] UserRecordDto dto, [FromServices] ICreateUserRecordCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/UserRecords/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserRecordDto dto, [FromServices] IEditUserRecordCommand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/UserRecords/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserRecordCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }
    }
}
