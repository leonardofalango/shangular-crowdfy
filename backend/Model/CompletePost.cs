public class CompletePost
{
    public int IdAuthor { get; set; }
    public string? AuthorName { get; set; }
    public string? Content { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Crowds { get; set; }
    public int? Comments { get; set; }
    public int IdPost { get; set; }
    public string? Photo { get; set; }
    public int? IdForum { get; set; }
    public string? ForumName { get; set; }
    public int? FKPost { get; set; }
    public string? Title { get; set; }
}