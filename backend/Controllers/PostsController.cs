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
        [FromServices] IRepository<Post> postRepo
    )
    {
        if (!await postRepo.Create(post))
            return StatusCode(503);
        
        return Ok();
    }
    
    [HttpGet("getById/{id}-{page}")]
    public async Task<ActionResult<List<PostDTO>?>?> GetById(
        int id, int page,
        [FromServices] IRepository<Post> postRepo,
        [FromServices] IPostService postService
    )
    {
        List<PostDTO?>? post = await postService.GetById(id, page);

        if (post == null)
            return StatusCode(404);
        
        return post!;
    }
    
    [HttpGet("{filters}+{page}")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPage(
        string filters,
        int page,
        [FromServices] IRepository<Post> postRepo,
        [FromServices] IPostService postService
    )
    {
        int[] filterArr = filters
            .Substring(0, filters.IndexOf("+"))
            .Split(",")
            .Select(
                filter => int.Parse(filter)
            ).ToArray();

        List<PostDTO> posts = await postService.GetPageWithCrowdsFilter(filterArr, page);

        if (posts.Count() == 0)
            return StatusCode(404);
        
        return posts;
    }

}
