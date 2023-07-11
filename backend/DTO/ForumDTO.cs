using backend.Model;

namespace backend.DataTransferObject;

public class ForumDTO
{
    public int Id { get; set; }
    public string? Creator { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }
    public bool Selected { get; set; }

    public ForumDTO(string? creator, DateTime? createdAt, string? title, string? description, string? photo, bool selected, int id)
    {
        Creator = creator;
        CreatedAt = createdAt;
        Title = title;
        Description = description;
        Photo = photo;
        Selected = selected;
        Id = id;
    }
}