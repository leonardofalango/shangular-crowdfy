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
[Route("")]
[EnableCors("MainPolicy")]
public class PostsController : ControllerBase
{
    private CrowdfyContext context;
    private int counter = -1;
    private int getCounter
    {
        get => counter++;
        set => this.counter = value;
    }
    public PostsController(CrowdfyContext ctt)
        => this.context = ctt;
    
    private IEnumerable<CompletePost> getFullJoin()
        => from p in context.Posts
                join u in context.Users
                    on p.Author equals u.Id
                        join f in context.Forums
                            on p.IdForum equals f.Id
        select new CompletePost
        {
            IdAuthor = u.Id,
            AuthorName = u.Username,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
            Crowds = p.Crowds,
            Comments = p.Comments,
            IdPost = p.Id,
            FKPost = p.IdPost,
            Photo = u.Photo,
            IdForum = p.IdForum,
            ForumName = f.Title,
            Title = p.Title
        };

    private IEnumerable<CompletePost> getOrderedFullJoin()
        => getFullJoin().OrderBy(e => e.CreatedAt);

    [HttpGet("")]
    public IEnumerable<CompletePost> Get()
        => GetIndex(0);
    
    [HttpGet("{indexStart}")]
    public IEnumerable<CompletePost> GetIndex(int indexStart)
        => getOrderedFullJoin()
            .SkipWhile(e => getCounter < indexStart)
            .Take(10);

    [HttpGet("{forum}/{indexStart}")]
    public IEnumerable<CompletePost> GetForum(string forum, int indexStart = 0)
        => getOrderedFullJoin()
        .Where(e => e.ForumName == forum)
        .SkipWhile(p => getCounter < indexStart);

    [HttpGet("post/{idPost}")]
    public IEnumerable<CompletePost> GetPost(int idPost)
    {
        IEnumerable<CompletePost> principalPost = getFullJoin().Where(e => e.IdPost == idPost);
        IEnumerable<CompletePost> comments = getFullJoin().Where(e => e.FKPost == idPost);

        return principalPost.ToList().AddMany(comments);
    }

    [HttpPost("post/add")]
    public ActionResult PostNewPost([FromBody] Post post)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data.");
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Ok();
    }

    [Route("page/{page}")]
    public IEnumerable<CompletePost> GetPage(int page)
        => getOrderedFullJoin()
            .Skip(page * 10)
            .Take(10);
    
}
