using backend.DataTransferObject;

namespace backend.Model.Interfaces;


public interface IPostService
{
    Task<List<PostDTO>> GetPageWithCrowds(int page, int itemPerPage=10);
    Task<List<PostDTO>> GetPageWithCrowdsFilter(int[] forumId, int page, int itemPerPage=10);
    Task<List<PostDTO?>?> GetById(int id, int page, int itemPerPage=10);
}