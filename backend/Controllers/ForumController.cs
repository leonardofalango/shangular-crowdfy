using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model.Services;
using backend.Model.Interfaces;
using Security_jwt;
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
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] IUserService userService,
        [FromServices] IJwtService jwt
    )
    {
        Forum forumToInsert = new Forum();

        forumToInsert.Title = forum.Title;
        forumToInsert.Creator = userService.GetByName(forum.Creator!).Id;
        forumToInsert.CreatedAt = forum.CreatedAt;
        forumToInsert.Description = forum.Description;
        forumToInsert.Photo = forum.Photo;
        
        if (!ModelState.IsValid)
            return BadRequest();

        if (!await forumRepo.Create(forumToInsert))
            return StatusCode(503);
            
        return Ok();
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete(
        [FromBody] Forum forum,
        [FromServices] IRepository<Forum> forumRepo
    )
    {
        if (!await forumRepo.Delete(forum))
            return StatusCode(503);
        return Ok();
    }

    [HttpGet("updateById/{id}")]
    public async Task<ActionResult> UpdateById(
        int id,
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] ForumService forumService
    )
    {
        Forum? f = await forumService.GetForumById(id);

        if (f == null)
            return StatusCode(404);

        if (!await forumRepo.Update(f))
            return StatusCode(503);
        
        return Ok();
    }

    [HttpPost("update")]
    public async Task<ActionResult> Update(
        [FromBody] Forum forum,
        [FromServices] IRepository<Forum> forumRepo
    )
    {
        if (!await forumRepo.Update(forum))
            return StatusCode(503);
        
        return Ok();
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<ForumDTO>> GetById(
        int id,
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] IForumService forumService
    )
    {
        ForumDTO? f = await forumService.GetById(id);
        if (f == null)
            return StatusCode(404);
        
        return f;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<ForumDTO>>> GetAll(
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] IForumService forumService
    )
    {
        List<ForumDTO> forums = await forumService.GetAll();

        if (forums.Count == 0)
        {
            return NotFound();
        }

        return Ok(forums);
    }

    [HttpGet("searchByName/{name}")]
    public async Task<ActionResult<List<ForumDTO>>> SearchByForum(
        string name,
        [FromServices] IForumService forumService
    )
    {
        List<ForumDTO> forums = await forumService.GetByName(name);

        if (forums.Count == 0)
        {
            return NotFound();
        }

        return Ok(forums);
    }

    [HttpGet("getSubscribedForums/{idUser}")]
    public async Task<ActionResult<List<ForumDTO>>> GetSubscribedForums(
        int idUser,
        [FromServices] IForumService forumService
    )
    {
        List<ForumDTO> subscribedForums = await forumService.GetSubscribedForums(idUser);
        
        if (subscribedForums.Count == 0)
        {
            return NotFound();
        }
        return Ok(subscribedForums);
    }

}