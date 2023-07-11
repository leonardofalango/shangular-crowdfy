using System.Linq.Expressions;
using System.Linq;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.DataTransferObject;

namespace backend.Model.Services;

public class PostService : IPostService
{
    private CrowdfyContext context;

    public PostService(CrowdfyContext ctt)
        => this.context = ctt;

    public async Task<List<PostDTO?>?> GetById(int id, int page, int itemPerPage)
    {
        var principalPost = 
            from like in this.context.UserXlikes
            group like by like.IdPost
            into g
            select new 
            {
                PostId = g.Key,
                Likes = g.Count()
            }
            into likesgroup
            where likesgroup.PostId == id
            join post in this.context.Posts
            on likesgroup.PostId equals post.Id
            join forum in this.context.Forums
            on  post.IdForum equals forum.Id
            join author in this.context.Users
            on post.Author equals author.Id
            select new PostDTO
            (
                post.Id,
                author.Username,
                post.Title,
                post.Content,
                post.Anex,
                post.CreatedAt,
                post.IdPost,
                likesgroup.Likes,
                forum.Title
            );
        var mainPost = await principalPost.FirstOrDefaultAsync();
        
        if (mainPost == null)
            return null;

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
            where post.IdPost == id
            join forum in this.context.Forums
            on  post.IdForum equals forum.Id
            join author in this.context.Users
            on post.Author equals author.Id
            select new PostDTO
            (
                post.Id,
                author.Username,
                post.Title,
                post.Content,
                post.Anex,
                post.CreatedAt,
                post.IdPost,
                likesgroup.Likes,
                forum.Title
            );

        var list = await postJoint
            .Skip(page * itemPerPage)
            .Take(itemPerPage)
            .ToListAsync();
        
        return list
            .Where(x => x != mainPost)
            .Prepend(mainPost)
            .ToList();
    }

    public async Task<List<PostDTO>> GetPageWithCrowds(int page, int itemPerPage = 10)
    {
        System.Console.WriteLine("getpageswithcrowds");

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
            join author in this.context.Users
            on post.Author equals author.Id
            select new PostDTO
            (
                post.Id,
                author.Username,
                post.Title,
                post.Content,
                post.Anex,
                post.CreatedAt,
                post.IdPost,
                likesgroup.Likes,
                forum.Title
            );

        System.Console.WriteLine("join?");
        var print = await postJoint.ToListAsync();
        foreach (var item in print)
            System.Console.WriteLine(item);            

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
            join author in this.context.Users
            on post.Author equals author.Id 
            select new PostDTO
            (
                post.Id,
                author.Username,
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

    public async Task<bool> Like(int idPost, int idUser, bool isLiked)
    {
        try {
            var post = await this.context.Posts
                .Where(p => p.Id == idPost)
                .FirstOrDefaultAsync();
            
            if (post == null)
                return false;

            var userXlike = await this.context.UserXlikes
                .Where(uxl => uxl.IdPost == idPost && uxl.IdUser == idUser)
                .FirstOrDefaultAsync();
            
            if (userXlike == null)
            {
                var newUserXlike = new UserXlike
                {
                    IdUser = idUser,
                    IdPost = idPost
                };

                this.context.UserXlikes.Add(newUserXlike);
            }
            else
            {
                this.context.UserXlikes.Remove(userXlike);
            }

            await this.context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}