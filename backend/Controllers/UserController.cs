using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model.Interfaces;
using backend.DataTransferObject;
using Security_jwt;

namespace backend.Controllers;


// ?Routes

[Route("user")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{
    [HttpPost("add")]
    public async Task<ActionResult> Add(
        [FromBody] InsertUser user,
        [FromServices] IRepository<User> userRepo
    )
    {
        User userToInsert = new User();

        userToInsert.BornDate = user.BornDate;
        userToInsert.Completename = user.Completename;
        userToInsert.IsAuth = user.IsAuth;
        userToInsert.Mail = user.Mail;
        userToInsert.Photo = user.Photo;
        userToInsert.HashCode = user.Password;
        userToInsert.Username = user.Username;

        bool isGuti = await userRepo.Create(userToInsert);

        if (!isGuti)
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
    public async Task<ActionResult<Jwt>> Login(
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
        
        
        //! return Ok(jwt);
        return new Jwt(){ Token=jwt };
    }

    [HttpPost("validateToken")]
    public async Task<ActionResult<UserDTO>> ValidateTokenReturnUser(
        [FromBody] Jwt token,
        [FromServices] IJwtService jwt
    ) => Ok(jwt.Validate<UserDTO>(token.Token));
}