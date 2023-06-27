using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace backend.Controllers;

// Routes
// localhost:5177/?index
// localhost:5177/?forum/?index
// localhost:5177/post/?idPost
// localhost:5177/post/add



[ApiController]
[Route("forum")]
[EnableCors("MainPolicy")]
public class ForumController : ControllerBase
{
    private CrowdfyContext context;

    public ForumController(CrowdfyContext ctt)
        => this.context = ctt;

    [HttpGet]
    public IEnumerable<Forum> GetAllForums()
        => context.Forums.Take(100);
    
    [HttpPost("add")]
    public ActionResult CreateForum([FromBody] Forum forum)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data.");
        
        context.Forums.Add(forum);
        context.SaveChanges();

        return Ok();
    }

    [Route("{userId}")]
    public IEnumerable<Forum> GetSubscribedForums(int userId) =>
        from forum in context.Forums
        where forum.IdUsers.Any(user => user.Id == userId)
        select forum;
    
    [Route("name/{forumName}")]
    public Forum? GetForumByName(string forumName)
        => context
            .Forums
            .FirstOrDefault(
                f => f.Title! == forumName
            );
}