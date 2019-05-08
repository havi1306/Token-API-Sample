using Code_Sample.Dots;
using Code_Sample.Entities;
using Code_Sample.Helpers;
using Code_Sample.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Code_Sample.Service
{
    public class UserService : IUserService
    {
        private readonly DBContext _dBContext;

        public UserService(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void CreateUser(User user)
        {
            _dBContext.Users.Add(user);
            _dBContext.SaveChanges();
        }

        public void DelectUser(User user)
        {
            _dBContext.Users.Remove(user);
            _dBContext.SaveChanges();
        }

        public string GenerateJSONWebToken(User user)
        {
            var claims = new[]
            {
               new Claim("Id", user.Id.ToString()),
                new Claim("Role", user.Role.Name)
            };
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret Key You Devise")),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<UserDto> GetAllUsers()
        {
            var user = _dBContext.Users.Include(x => x.Role).Select(x => new UserDto()
            {
                UserName = x.UserName,
                Password = x.Password,
                RoleId = x.RoleID,
            })
            .ToList();
            return user;
        }

        public UserDto GetUser(int? id)
        {
            var user = _dBContext.Users.Where(x => x.Id == id).Select(x => new UserDto()
            {
                UserName = x.UserName,
                Password = x.Password,
                RoleId = x.RoleID,
            })
            .FirstOrDefault();
            return user;
        }

        public User GetUser(string userName, string password)
        {
            var user = _dBContext.Users.Include(x => x.Role)
                .FirstOrDefault(u => u.UserName == userName && u.Password == password);
            return user;
        }

        public User GetUserById(int? id)
        {
            var user = _dBContext.Users.Find(id);
            return user;
           
        }

        public void UpdateUser(User user)
        {
            _dBContext.Users.Update(user);
            _dBContext.SaveChanges();
        }
    }
}