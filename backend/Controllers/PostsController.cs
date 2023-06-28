using backend.Model;
using backend.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Linq.Expressions;
using backend.Model.Services;

namespace backend.Controllers;

// Routes
// localhost:5177/?index
// localhost:5177/?forum/?index
// localhost:5177/post/?idPost
// localhost:5177/post/add



[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]
public class PostsController : ControllerBase
{
    [HttpPost("add")]
    public async Task<ActionResult> Add(
        [FromBody] Post post,
        [FromServices] PostService postService
    )
    {
        if (!await postService.Create(post))
            return StatusCode(503);
        
        return Ok();
    }
    
    [HttpGet("getById/{id}")]
    public async Task<ActionResult<Post>> GetById(
        int id,
        [FromServices] PostService postService
    )
    {
        Post? post = await postService.GetById(id);

        if (post == null)
            return StatusCode(404);
        
        return post;
    }
    
    [HttpGet("{page}")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPage(
        int page,
        [FromServices] PostService postService
    )
    {
        List<Post> posts = await postService.GetPage(page);

        if (posts.Count() == 0)
            return StatusCode(404);
        
        return posts;
    }

}
