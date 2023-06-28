using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model.Services;
using backend.Model.Interfaces;
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
        [FromServices] IService<User> userService
    )
    {
        if (!await userService.Create(user))
            return StatusCode(503);
        
        return Ok();
    }
    
    [HttpGet("getById/{id}")]
    public async Task<ActionResult<User>> GetById(
        int id,
        [FromServices] IService<User> userService
    )
    {
        User? user = await userService.GetById(id);

        if (user == null)
            return StatusCode(404);
        
        return user;
    }
}