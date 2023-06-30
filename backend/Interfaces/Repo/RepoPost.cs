using System.Linq.Expressions;
using backend.Model;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RepoPost : IRepository<Post>
{
    private CrowdfyContext context;

    public RepoPost(CrowdfyContext ctt)
        => this.context = ctt;

     public async Task<bool> Create(Post obj)
    {
        try 
        {
            await this.context.Posts.AddAsync(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> Delete(Post obj)
    {
        try 
        {
            this.context.Posts.Remove(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<List<Post>> Filter(Expression<Func<Post, bool>> exp)
        => await this.context.Posts.Where(exp).ToListAsync();
    
    public async Task<bool> Update(Post obj)
    {
        try 
        {
            var originalObj = this.context.Posts.First(post => post.Id == obj.Id);

            this.context.Posts.Update(obj);
            await this.context.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
}