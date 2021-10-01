using Application.DTO;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Core
{
    public class JwtManager
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;

        public JwtManager(RadContext con, IMapper mapper)
        {
            _con = con;
            _mapper = mapper;
        }

        public Res MakeToken(string email, string password)
        {
            var user = _con.Users.Include(x=>x.Role)
                .FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                return null;
            }


            var actor = new JwtActor
            {
                Id = user.Id,
                RoleId = user.RoleId,
                Identity = user.Email
            };

            var issuer = "asp_api";
            var secretKey = "ThisIsMyVerySecretKey";
            var claims = new List<Claim> // Jti : "", 
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, "asp_api", ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, issuer),
                new Claim("ActorData", JsonConvert.SerializeObject(actor), ClaimValueTypes.String, issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(3600),
                signingCredentials: credentials);
            Res res = new Res();
            res.User = _mapper.Map<UserDto>(user);
            res.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return res;
        }
        public class Res
        {
            public string Token { get; set; }
            public UserDto User { get; set; }
        }
    }
}
