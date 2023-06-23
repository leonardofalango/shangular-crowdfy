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

    [HttpGet("")]
    public IEnumerable<Forum> GetAllForum()
        => context.Forums.Where(e => true);
    
    [Route("add")]
    public ActionResult CreateForum(Forum forum)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data.");
        
        context.Forums.Add(forum);
        context.SaveChanges();

        return Ok();
    }
    
}