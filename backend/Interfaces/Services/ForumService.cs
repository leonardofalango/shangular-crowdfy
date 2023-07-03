using System.Linq.Expressions;
using backend.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.DataTransferObject;

namespace backend.Model.Services;

public class ForumService : IForumService
{
    private CrowdfyContext context;

    public ForumService(CrowdfyContext ctt)
        => this.context = ctt;

    public Task<ForumDTO?> GetById(int id)
    {
        var query = from forum in this.context.Forums
        where forum.Id == id
        join user in this.context.Users
            on forum.Creator equals user.Id
        select new ForumDTO(
            user.Username,
            forum.CreatedAt,
            forum.Title,
            forum.Description,
            forum.Photo
        );

        return query.FirstOrDefaultAsync();
    }


    
    public Task<List<ForumDTO>> GetAll()
    {
        var query = from forums in context.Forums
                    join authors in context.Users
                    on forums.Creator equals authors.Id
                    select new ForumDTO(
                        authors.Username,
                        forums.CreatedAt,
                        forums.Title,
                        forums.Description,
                        forums.Photo
                    );

        return query.ToListAsync();
    }

    public Task<List<ForumDTO>> GetByName(string name)
    {
        var query = from f in this.context.Forums
                    where f.Title!.Contains(name)
                    join users in this.context.Users
                        on f.Creator equals users.Id
                        select new ForumDTO(
                            users.Username,
                            f.CreatedAt,
                            f.Title,
                            f.Description,
                            f.Photo
                        );
        
        return query.ToListAsync();
    }

    public Task<List<ForumDTO>> GetSubscribedForums(int userId)
    {
        var query = from uXf in this.context.UserXforums
                    where uXf.Id == userId
                    join f in this.context.Forums
                        on uXf.IdForum equals f.Id
                    join u in this.context.Users
                        on uXf.IdUser equals u.Id
                    select new ForumDTO(
                        u.Username,
                        f.CreatedAt,
                        f.Title,
                        f.Description,
                        f.Photo
                    );
        
        return query.ToListAsync();
    }

    public Task<Forum?> GetForumById(int id)
        => this.context.Forums
            .FirstOrDefaultAsync(
                forum =>
                forum.Id == id
            );
}