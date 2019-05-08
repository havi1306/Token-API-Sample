using Code_Sample.Dots;
using Code_Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code_Sample.IService
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();

        UserDto GetUser(int? id);

        User GetUser(string userName, string password);

        User GetUserById(int? id);

        string GenerateJSONWebToken(User user);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DelectUser(User user);
    }
}
