using backend.Model;

namespace backend.DataTransferObject;

public class ForumDTO
{
    public string? Creator { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }


}