using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
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
    public class LogsController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;

        public LogsController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/Roles
        [HttpGet]
        public IActionResult Get([FromQuery] LogSearchDto catSDto, [FromServices] IGetLogsQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/Roles/5
        [HttpGet("{id}", Name = "GetLog")]
        public IActionResult Get(int id, [FromServices] IGetLogQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

    }
}
