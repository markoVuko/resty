using API.Core;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly JwtManager _manager;

        public LoginController(JwtManager manager)
        {
            _manager = manager;
        }

        // POST api/<Login>
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            var tokenRes = _manager.MakeToken(request.Email, GetMd5Hash(request.Password));
            if (tokenRes==null||string.IsNullOrWhiteSpace(tokenRes.Token))
            {
                return Unauthorized();
                //return Ok(GetMd5Hash(request.Password));
            }
            return Ok(tokenRes);
        }

        public string GetMd5Hash(string input)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
