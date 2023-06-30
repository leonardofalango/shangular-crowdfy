using System.Linq.Expressions;
using backend.DataTransferObject;

namespace backend.Model.Interfaces;


public interface IForumService
{
    Task<Forum?> GetById (int id);

    Task<List<Forum>> GetAll ();
}