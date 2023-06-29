using backend.Model;
using backend.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using backend.Model.Services;
using backend.DataTransferObject;

namespace backend.Controllers;


[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]
public class PostsController : ControllerBase
{
    [HttpPost("add")]
    public async Task<ActionResult> Add(
        [FromBody] Post post,
        [FromServices] IPostService postService
    )
    {
        if (!await postService.Create(post))
            return StatusCode(503);
        
        return Ok();
    }
    
    [HttpGet("getById/{id}")]
    public async Task<ActionResult<Post>> GetById(
        int id,
        [FromServices] IPostService postService
    )
    {
        Post? post = await postService.GetById(id);

        if (post == null)
            return StatusCode(404);
        
        return post;
    }
    
    [HttpGet("{filters}+{page}")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPage(
        int[] filters,
        int page,
        [FromServices] IPostService postService
    )
    {
        List<PostDTO> posts = await postService.GetPageWithCrowdsFilter(filters, page);

        if (posts.Count() == 0)
            return StatusCode(404);
        
        return posts;
    }

}
