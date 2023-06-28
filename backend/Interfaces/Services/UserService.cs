using System.Linq.Expressions;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using security_jwt;

namespace backend.Model.Services;

public class UserService : IService<User>
{
    private CrowdfyContext context;

    public UserService(CrowdfyContext ctt)
        => this.context = ctt;

    public async Task<bool> Create(User obj)
    {
        try 
        {
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