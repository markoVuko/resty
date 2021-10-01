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
    public class CategoriesController : ControllerBase
    {
        private readonly IAppActor _actor;
        private readonly ExecutionAgent _exec;
        public CategoriesController(IAppActor actor, ExecutionAgent exec)
        {
            _actor = actor;
            _exec = exec;
        }
        // GET: api/Categories
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearchDto catSDto, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, catSDto));
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id, [FromServices] IGetCategoryQuery query)
        {
            return Ok(_exec.ExecuteQuery(query, id));
        }

        // POST: api/Categories
        [HttpPost]
        public void Post([FromBody] CategoryDto dto, [FromServices] ICreateCategoryCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CategoryDto dto, [FromServices] IEditCategoryCommand comm)
        {

            dto.Id = id;
            _exec.ExecuteCommand(comm, dto);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand comm)
        {
            _exec.ExecuteCommand(comm, id);
            return NoContent();
        }
    }
}
