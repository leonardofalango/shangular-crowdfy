using System.Linq.Expressions;
using backend.DataTransferObject;

namespace backend.Model.Interfaces;


public interface IForumService
{
    Task<Forum?> GetForumById (int id);
    Task<ForumDTO?> GetById (int id, int idUser);
    Task<List<ForumDTO>> GetAll ();
    Task<List<ForumDTO>> GetByName(string name, int userId);
    Task<List<ForumDTO>> GetSubscribedForums(int userId);
    Task<bool> Subscribe(int userId, int forumId);
}