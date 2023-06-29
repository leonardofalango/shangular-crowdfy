using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using backend.Model.Interfaces;
using backend.DataTransferObject;

namespace backend.Model.Services;

public class ForumService : IForumService
{
    private CrowdfyContext context;

    public ForumService(CrowdfyContext ctt)
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

    public async Task<Forum?> GetById(int id)
    {
        Forum? req = new Forum();
        
        req = await this.context.Forums.FirstOrDefaultAsync(Forum => Forum.Id == id);
        
        return req;
    }
    
    public async Task<IEnumerable<Forum>> Take(int quantity)
    {
        var query = from forum in this.context.Users.
                    on 
    }


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