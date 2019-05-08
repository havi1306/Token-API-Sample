using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code_Sample.IService;
using Microsoft.AspNetCore.Mvc;

namespace Code_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("token")]
        [HttpPost]
        public IActionResult GetToken(String UserName, string PassWord)
        {
            var user = _userService.GetUser(UserName, PassWord);
            if(user != null)
            {
                var tokenString = _userService.GenerateJSONWebToken(user);

                return Ok(new { token = tokenString });
            }
            return NotFound();
        }
    }
}