using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Security.Cryptography;
using System.Text;

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
    
    [Route("login")]
    public JWT.JwtParts? Login([FromBody] Login usr)
    {
        User? user = context.Users.FirstOrDefault(
            u => u.Mail == usr.login || u.Username == usr.login
        );

        if (user == null)
            return null;
        
        string databasePassword = hashEncode(usr.password + user.Salt);
        
    }

    private string hashEncode(string str)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(str);
        var hasBytes = sha.ComputeHash(bytes);
        var hash = Convert.ToBase64String(hasBytes);
        
        return hash;
    }
    
}