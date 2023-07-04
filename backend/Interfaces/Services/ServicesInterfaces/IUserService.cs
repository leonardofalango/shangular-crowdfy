using backend.Security.Hash;
using backend.DataTransferObject;

namespace backend.Model.Interfaces;

public interface IUserService
{
    //? Returns jwt to front
    Task<UserDTO?> GetUserByLogin(LoginDTO log);
    string? ApplyHash(string pass, IHashAlgoritm alg);
    Task<List<UserDTO>> GetPage(int page, int itemPerPage);
    Task<List<UserDTO>> SearchByName(string username);
    Task<User?> GetByName(string username);
    Task<User?> GetById(int id);
}