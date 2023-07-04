using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model.Interfaces;
using backend.DataTransferObject;
using security_jwt;

namespace backend.Controllers;


// ?Routes

[Route("user")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{
    [HttpPost("add")]
    public async Task<ActionResult> Add(
        [FromBody] User user,
        [FromServices] IRepository<User> userRepo
    )
    {
        if (!await userRepo.Create(user))
            return StatusCode(503);
        
        return Ok();
    }
    
    [HttpGet("getById/{id}")]
    public async Task<ActionResult<User>> GetById(
        int id,
        [FromServices] IRepository<User> userRepo,
        [FromServices] IUserService userService
    )
    {
        User? user = await userService.GetById(id);

        if (user == null)
            return StatusCode(404);
        
        return user;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] LoginDTO login,
        [FromServices] IUserService userRepo,
        [FromServices] IJwtService jwtService
    )
    {
        #pragma warning disable

        UserDTO? user = await userRepo.GetUserByLogin(login);
        
        if (user == null)
            return StatusCode(404);

        string jwt = jwtService.GetToken<UserDTO>(user);

        if (jwt == "")
            return StatusCode(401);
        
        
        return Ok(jwt);
    }
}