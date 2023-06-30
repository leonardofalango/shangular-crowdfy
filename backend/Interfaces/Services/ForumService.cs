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

    public Task<Forum?> GetById(int id)
        => this.context.Forums.FirstOrDefaultAsync(
            forum => forum.Id == id
        );
    
}