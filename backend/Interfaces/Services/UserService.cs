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

    public async Task<UserDTO?> GetUserByLogin(LoginDTO log)
    {
        User? user = this.context.Users.FirstOrDefault(
            user => 
                user.Username == log.Login || user.Mail == log.Login
        );

        if (user == null)
            return null;

        
        string passwordSalt = log.Password + user.Salt;

        string pass = ApplyHash(passwordSalt, new Base64SHA256());

        System.Console.WriteLine(pass);

        User? u = await this.context
            .Users
            .Where(
                u => u.Id == user.Id
            )
            .FirstOrDefaultAsync(
                user =>
                user.HashCode == pass
            );

        
        return new UserDTO(
            u.Completename,
            u.Username,
            u.Photo,
            u.BornDate,
            u.Mail,
            u.IsAuth
        );
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