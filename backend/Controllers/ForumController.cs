using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model.Services;
using backend.Model.Interfaces;
using security_jwt;
using backend.DataTransferObject;

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
    [HttpPost("add")]
    public async Task<ActionResult> Add(
        [FromBody] ForumDTO forum,
        [FromServices] IForumService forumService,
        [FromServices] IJwtService jwt
    )
    {
        // jwt.Validate(input do front)
        // jwt.GetToken()
        
        if (!ModelState.IsValid)
            return BadRequest();

        if (!await forumService.Create(forum))
            return StatusCode(503);
            
        return Ok();
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete(
        [FromBody] Forum forum,
        [FromServices] IService<Forum> forumService
    )
    {
        if (!await forumService.Delete(forum))
            return StatusCode(503);
        return Ok();
    }

    [HttpGet("updateById/{id}")]
    public async Task<ActionResult> UpdateById(
        int id,
        [FromServices] IService<Forum> forumService
    )
    {
        Forum? f = await forumService.GetById(id);

        if (f == null)
            return StatusCode(404);

        if (!await forumService.Update(f))
            return StatusCode(503);
        
        return Ok();
    }

    [HttpPost("update")]
    public async Task<ActionResult> Update(
        [FromBody] Forum forum,
        [FromServices] IService<Forum> forumService
    )
    {
        if (!await forumService.Update(forum))
            return StatusCode(503);
        
        return Ok();
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<Forum>> GetById(
        int id,
        [FromServices] IService<Forum> forumService
    )
    {
        Forum? f = await forumService.GetById(id);
        if (f == null)
            return StatusCode(404);
        
        return f;
    }

}