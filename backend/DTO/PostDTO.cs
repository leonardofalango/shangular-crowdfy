namespace backend.DataTransferObject;

public class PostDTO
{
    public int Id { get; set; }

    public string? Author { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Anex { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IdPost { get; set; }
    public int Likes { get; set; }
    public string? ForumName { get; set; }

    public PostDTO(int id, string? author, string? title, string? content, string? anex, DateTime? createdAt, int? idPost, int likes, string? forumName)
    {
        Id = id;
        Author = author;
        Title = title;
        Content = content;
        Anex = anex;
        CreatedAt = createdAt;
        IdPost = idPost;
        Likes = likes;
        ForumName = forumName;
    }
}