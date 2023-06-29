using System.Linq.Expressions;
using backend.Model.Interfaces;
using backend.DataTransferObject;
using Microsoft.EntityFrameworkCore;
using backend.Security.Hash;

namespace backend.Model.Services;

public class UserService : IUserService
{
    private CrowdfyContext context;

    public UserService(CrowdfyContext ctt)
        => this.context = ctt;

    public string? ApplyHash(string pass, IHashAlgoritm alg)
        => alg.ToHash(pass);

    public async Task<bool> Create(User obj)
    {
        try 
        {
            obj.Salt = new Security
                .TextSalt()
                .GetSalt();

            await this.context.Users.AddAsync(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> Delete(User obj)
    {
        try 
        {
            this.context.Users.Remove(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<List<User>> Filter(Expression<Func<User, bool>> exp)
        => await this.context.Users.Where(exp).ToListAsync();

    public async Task<User?> GetById(int id)
    {
        User? req = new User();
        
        req = await this.context.Users.FirstOrDefaultAsync(User => User.Id == id);
        
        return req;
    }

    #pragma warning disable
    public async Task<string?> GetJwt(LoginDTO log)
    {
        #pragma warning disable
        string? salt = this.context.Users.FirstOrDefault(
            user => 
                user.Username == log.login || user.Mail == log.login
        ).Salt;

        if (salt is null)
            return null;
        
        string passwordSalt = log.password + salt;

        return ApplyHash(passwordSalt, new Base64SHA256());
    }

    public async Task<IEnumerable<User>> Take(int quantity)
        => await this.context.Users.Take(quantity).ToListAsync();

    public async Task<bool> Update(User obj)
    {
        try 
        {
            var originalObj = this.context.Users.First(post => post.Id == obj.Id);

            this.context.Users.Update(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}