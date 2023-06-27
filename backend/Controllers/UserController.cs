using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace backend.Controllers;

// ?Routes

[Route("user")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{    
    private CrowdfyContext context;

    public UserController(CrowdfyContext ctt)
        => this.context = ctt;
        
    //! FIND USER BY ID
    [Route("{userId}")]
    public User? GetUser(int userId)
        => this.context
            .Users
            .FirstOrDefault(
                e => e.Id == userId
            );
    
    [Route("")]
    public IEnumerable<User> GetAllUsers()
        => this.context
            .Users
            .Take(100);
    
}