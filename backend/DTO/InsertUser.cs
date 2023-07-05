using backend.DataTransferObject;

public class InsertUser : UserDTO
{
    public InsertUser(string? completename, string? username, string? photo, DateTime? bornDate, string? mail, int? isAuth, string password) : base(completename, username, photo, bornDate, mail, isAuth)
    {
        this.Password = password;
    }
    public string Password { get; set; }
}