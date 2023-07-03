using System.Linq.Expressions;
using backend.DataTransferObject;

namespace backend.Model.Interfaces;


public interface IForumService
{
    Task<Forum?> GetForumById (int id);
    Task<ForumDTO?> GetById (int id);
    Task<List<ForumDTO>> GetAll ();
    Task<List<ForumDTO>> GetByName(string name);
    Task<List<ForumDTO>> GetSubscribedForums(UserDTO user);
}