using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.BAL.Interface;
using Todo.Models.RequestDTO;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IUserInterface _userInterface;
        public AuthController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(LoginRequest l1)
        {
            var result = _userInterface.Authentication(l1);
            if (result.statuscode == 100)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
         [HttpPost("Register")]
         public IActionResult UserRegister(RegisterRequest l1)
         {
             var result = _userInterface.Authorization(l1);
             if (result.statuscode == 100 || result.statuscode == 101)
             {
                 return BadRequest(result);
             }
             return Ok(result);
        }
    }
}
