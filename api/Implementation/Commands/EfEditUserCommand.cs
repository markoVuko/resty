using Application;
using Application.Commands;
using Application.DTO;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Commands
{
    public class EfEditUserCommand : IEditUserCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly IAppActor _actor;
        private readonly EditUserValidator _val;
        public EfEditUserCommand(RadContext con, IMapper mapper, IAppActor actor, EditUserValidator val)
        {
            _con = con;
            _val = val;
            _mapper = mapper;
            _actor = actor;
        }
        public int Id => 25;

        public string Name => "Edit User Command";

        public List<int> AllowedRoles => new List<int> { 1, 2 };

        public void Execute(RegisterUserDto req)
        {
            if (_actor.Id == req.Id || _actor.RoleId == 1)
            {
                _val.ValidateAndThrow(req);

                var user = _con.Users.Where(x => x.IsDeleted == false && x.Id == req.Id).First();

                if (user == null)
                {
                    throw new InvalidEntityException(req.Id, typeof(User));
                }

                if (!string.IsNullOrWhiteSpace(req.FirstName))
                {
                    user.FirstName = req.FirstName;
                }

                if (!string.IsNullOrWhiteSpace(req.LastName))
                {
                    user.LastName = req.LastName;
                }

                if (!string.IsNullOrWhiteSpace(req.Email))
                {
                    user.Email = req.Email;
                }

                if (!string.IsNullOrWhiteSpace(req.Address))
                {
                    user.Address = req.Address;
                }
                if (!string.IsNullOrWhiteSpace(req.Password))
                {
                    user.Password = GetMd5Hash(req.Password);
                }

                user.ModifiedAt = DateTime.UtcNow;
                _con.SaveChanges();

            }
            else
            {
                throw new UnauthorizedAccessException();
            }
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
}
