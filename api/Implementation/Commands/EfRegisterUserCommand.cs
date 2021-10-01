using Application;
using Application.Commands;
using Application.DTO;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Commands
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        private readonly RadContext _con;
        private readonly IMapper _mapper;
        private readonly RegisterUserValidator _val;
        private readonly IEmailSender _sender;

        public EfRegisterUserCommand(RadContext con, IMapper mapper, RegisterUserValidator val, IEmailSender sender)
        {
            _con = con;
            _mapper = mapper;
            _val = val;
            _sender = sender;
        }
        public int Id => 5;

        public string Name => "Register User Command";

        public List<int> AllowedRoles => new List<int> { 0 };

        public void Execute(RegisterUserDto req)
        {
            _val.ValidateAndThrow(req);

            var user = new User
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                Password = GetMd5Hash(req.Password),
                Address = req.Address,
                RoleId = 2,
                DateOfBirth = req.DateOfBirth,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                IsActive = true
            };

            _con.Users.Add(user);
            _con.SaveChanges();

            _sender.sendEmail(new EmailDto
            {
                EmailTo = req.Email,
                Content = "You have successfully registered on our site!",
                Subject = "You have been registered"

            });
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
