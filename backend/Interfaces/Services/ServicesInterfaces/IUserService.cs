using backend.Security.Hash;
using backend.DataTransferObject;

namespace backend.Model.Interfaces;

public interface IUserService : IService<User>
{
    //? Returns jwt to front
    public Task<string?> GetJwt(LoginDTO log);

    public string? ApplyHash(string pass, IHashAlgoritm alg);
    
}