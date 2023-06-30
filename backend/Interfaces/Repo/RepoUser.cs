using System.Linq.Expressions;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Model.Services;

public class RepoUser : IRepository<User>
{
    private CrowdfyContext context;

    public RepoUser(CrowdfyContext ctt)
        => this.context = ctt;

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