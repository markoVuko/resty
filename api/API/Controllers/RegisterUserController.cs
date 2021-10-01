using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly ExecutionAgent _exec;
        public RegisterUserController(ExecutionAgent exec)
        {
            _exec = exec;
        }

        // POST: api/RegisterUser
        [HttpPost]
        public void Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand comm)
        {
            _exec.ExecuteCommand(comm, dto);
        }
    }
}