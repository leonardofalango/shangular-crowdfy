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
            .Split(",")
            .Select(
                filter => int.Parse(filter)
            ).ToArray();

        List<PostDTO> posts = await postService.GetPageWithCrowdsFilter(filterArr, page);

        if (posts.Count() == 0)
            return StatusCode(404);
        
        return posts;
    }

    // route to handle like and unlike
    [HttpPost("like")]
    public async Task<ActionResult> Like(
        [FromBody] Like like,
        [FromServices] IRepository<Like> likeRepo,
        [FromServices] IPostService postService
    )
    {
        if (!await likeRepo.Create(like))
            return StatusCode(503);
        
        await postService.Like(like.PostId, like.UserId, like.IsLiked);

        return Ok();
    }

}
