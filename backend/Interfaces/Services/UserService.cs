using backend.Model.Interfaces;
using backend.DataTransferObject;
using Microsoft.EntityFrameworkCore;
using backend.Security.Hash;
#pragma warning disable

namespace backend.Model.Services;
public class UserService : IUserService
{
    private CrowdfyContext context;

    public UserService(CrowdfyContext ctt)
        => this.context = ctt;

    public string? ApplyHash(string pass, IHashAlgoritm alg)
        => alg.ToHash(pass);

    public Task<List<UserDTO>> SearchByName(string username)
        => this.context.Users
            .Where(
                user =>
                user.Completename.Contains(username) ||
                user.Username.Contains(username)
            ).Select(user => new UserDTO(
                user.Completename,
                user.Username,
                user.Photo,
                user.BornDate,
                user.Mail,
                user.IsAuth
            )).ToListAsync();

    public async Task<string?> GetJwt(LoginDTO log)
    {
        string? salt = this.context.Users.FirstOrDefault(
            user => 
                user.Username == log.login || user.Mail == log.login
        ).Salt;

        if (salt is null)
            return null;
        
        string passwordSalt = log.password + salt;

        return ApplyHash(passwordSalt, new Base64SHA256());
    }

    public Task<List<UserDTO>> GetPage(int page, int itemPerPage = 10)
        => this.context.Users
            .Skip(page * itemPerPage)
            .Take(itemPerPage).Select(
                user =>
                new UserDTO(
                    user.Completename,
                    user.Username,
                    user.Photo,
                    user.BornDate,
                    user.Mail,
                    user.IsAuth
                )
            ).ToListAsync();

    public Task<User?> GetById(int id)
        => this.context.Users
            .FirstOrDefaultAsync(
                user =>
                user.Id == id
            );

    public Task<User?> GetByName(string name)
        => this.context.Users
            .FirstOrDefaultAsync(
                user =>
                user.Username == name ||
                user.Completename == name
            );

}