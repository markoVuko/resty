using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.DTO;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;
        public SuppliersController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/Suppliers
        [HttpGet]
        public IActionResult Get([FromQuery] SupplierSearchDto catSDto, [FromServices] IGetSuppliersQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}", Name = "GetSupplier")]
        public IActionResult Get(int id, [FromServices] IGetSupplierQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/Suppliers
        [HttpPost]
        public void Post([FromBody] SupplierDto dto, [FromServices] ICreateSupplierCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/Suppliers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SupplierDto dto, [FromServices] IEditSupplierCommand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteSupplierCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }
    }
}
