using System.Linq.Expressions;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.DataTransferObject;

namespace backend.Model.Services;

public class PostService
{
    private CrowdfyContext context;

    public PostService(CrowdfyContext ctt)
        => this.context = ctt;

   
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
    
    public async Task<List<PostDTO>> GetPageWithCrowds(int page, int itemPerPage = 10)
    {
        var postJoint =
            from like in this.context.UserXlikes
            group like by like.IdPost
            into g
            select new 
            {
                PostId = g.Key,
                Likes = g.Count()
            }
            into likesgroup
            join post in this.context.Posts
            on likesgroup.PostId equals post.Id
            join forum in this.context.Forums
            on  post.IdForum equals forum.Id
            select new PostDTO
            (
                post.Id,
                post.Author,
                post.Title,
                post.Content,
                post.Anex,
                post.CreatedAt,
                post.IdPost,
                likesgroup.Likes,
                forum.Title
            );

        return await postJoint
            .Skip(page * itemPerPage)
            .Take(itemPerPage)
            .ToListAsync();
    }

    public async Task<List<PostDTO>> GetPageWithCrowdsFilter(int[] idForums, int page, int itemPerPage)
    {
        var postJoint =
            from like in this.context.UserXlikes
            group like by like.IdPost
            into g
            select new 
            {
                PostId = g.Key,
                Likes = g.Count()
            }
            into likesgroup
            join post in this.context.Posts
            on likesgroup.PostId equals post.Id
            where idForums.Any(fkForum => fkForum == post.Id)
            join forum in this.context.Forums
            on  post.IdForum equals forum.Id
            select new PostDTO
            (
                post.Id,
                post.Author,
                post.Title,
                post.Content,
                post.Anex,
                post.CreatedAt,
                post.IdPost,
                likesgroup.Likes,
                forum.Title
            );


        return await postJoint.ToListAsync();
    }
}