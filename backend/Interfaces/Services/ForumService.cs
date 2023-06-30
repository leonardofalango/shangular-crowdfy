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
        throw new NotImplementedException();
    }

    
}