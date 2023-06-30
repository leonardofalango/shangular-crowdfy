namespace backend.DataTransferObject;

public class UserDTO
{
    public int Id { get; set; }
    public string? Completename { get; set; }

    public string? Username { get; set; }

    public string? Photo { get; set; }

    public DateTime? BornDate { get; set; }

    public string? Mail { get; set; }

    public int? IsAuth { get; set; }

    public UserDTO(string? completename, string? username, string? photo, DateTime? bornDate, string? mail, int? isAuth)
    {
        Completename = completename;
        Username = username;
        Photo = photo;
        BornDate = bornDate;
        Mail = mail;
        IsAuth = isAuth;
    }
}