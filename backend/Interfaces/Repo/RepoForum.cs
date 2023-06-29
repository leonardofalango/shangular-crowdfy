

using System.Linq.Expressions;
using backend.Model;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RepoForum : IRepository<Forum>
{
    private CrowdfyContext context;

    public RepoForum(CrowdfyContext ctt)
        => this.context = ctt;

     public async Task<bool> Create(Forum obj)
    {
        try 
        {
            await this.context.Forums.AddAsync(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> Delete(Forum obj)
    {
        try 
        {
            this.context.Forums.Remove(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<List<Forum>> Filter(Expression<Func<Forum, bool>> exp)
        => await this.context.Forums.Where(exp).ToListAsync();
    public async Task<bool> Update(Forum obj)
    {
        try 
        {
            var originalObj = this.context.Forums.First(post => post.Id == obj.Id);

            this.context.Forums.Update(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}