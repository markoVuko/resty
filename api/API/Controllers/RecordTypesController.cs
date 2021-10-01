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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecordTypesController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public RecordTypesController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/RecordTypes
        [HttpGet]
        public IActionResult Get([FromQuery] RecordTypeSearchDto catSDto, [FromServices] IGetRecordTypesQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/RecordTypes/5
        [HttpGet("{id}", Name = "GetRecordType")]
        public IActionResult Get(int id, [FromServices] IGetRecordTypeQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/RecordTypes
        [HttpPost]
        public void Post([FromForm] RecordTypeDto dto, [FromServices] ICreateRecordTypeCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/RecordTypes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] RecordTypeDto dto, [FromServices] IEditRecordTypeCommand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/RecordTypes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRecordTypeCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }

    }
}
