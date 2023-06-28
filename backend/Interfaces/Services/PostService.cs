using System.Linq.Expressions;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Model.Services;

public class PostService : IService<Post>
{
    private CrowdfyContext context;

    public PostService(CrowdfyContext ctt)
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

    public async Task<Post?> GetById(int id)
    {
        Post? req = new Post();
        
        req = await this.context.Posts.FirstOrDefaultAsync(Post => Post.Id == id);
        
        return req;
    }

    public async Task<IEnumerable<Post>> Take(int quantity)
        => await this.context.Posts.Take(quantity).ToListAsync();

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

    public async Task<List<Post>> GetPage(int page, int itemPerPage = 10)
        => await this.context.Posts.Skip(page * itemPerPage).Take(page).ToListAsync();
    
}