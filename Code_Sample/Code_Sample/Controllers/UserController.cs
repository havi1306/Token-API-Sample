using Code_Sample.Dots;
using Code_Sample.Entities;
using Code_Sample.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Code_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetByID(int? id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [Route("Create")]
        [Authorize(Policy = "admin")]
        [HttpPost]
        public IActionResult CreateUser([FromBody]UserDto userDto)
        {
            if(userDto.UserName == "string" || userDto.Password == "string" || userDto.RoleId == 0)
            {
                return BadRequest(new { error = "--------error----------" });
            }
            var user = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                RoleID = userDto.RoleId,
            };
            _userService.CreateUser(user);
            return Ok(new { success = "-----------success------------" });
        }
        [Route("update/{id}")]
        [Authorize(Policy ="admin")]
        [HttpPut]
        public IActionResult  UpdateUser(int? id ,[FromBody] UserDto userDto)
        {
            var user = _userService.GetUserById(id);
            var UserName = userDto.UserName;
            var Password = userDto.Password;
            var RoleID = userDto.RoleId;
            if(UserName == "string")
            {
                UserName = user.UserName;
            }
            if(Password == "string")
            {
                Password = user.Password;
            }
            if(RoleID == 0)
            {
                RoleID = user.RoleID;
            }
            if(user != null)
            {
                user.UserName = UserName;
                user.Password = Password;
                user.RoleID = RoleID;

                _userService.UpdateUser(user);
                return Ok(new { success = "------updated successfully-----" });
            }
            return BadRequest(new { error = "-----User is not exist-----" });
        }
       [Route("delete/{id}")]
       [Authorize("admin")]
       [HttpDelete]
       public IActionResult DeleteUser(int? id)
        {
            var user = _userService.GetUserById(id);

            if(user != null)
            {
                _userService.DelectUser(user);
                return Ok(new { success = "----Delete successully ----" });
            }
            return BadRequest(new { error = "-----User is not exist----" });
        }

    }
}