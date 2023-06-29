using backend.DataTransferObject;

namespace backend.Model.Interfaces;


public interface IPostService : IService<Post>
{
    public Task<List<Post>> GetPage(int page, int itemPerPage=10);
    public Task<List<PostDTO>> GetPageWithCrowds(int page, int itemPerPage=10);
    public Task<List<PostDTO>> GetPageWithCrowdsFilter(int[] forumId, int page, int itemPerPage=10);
}