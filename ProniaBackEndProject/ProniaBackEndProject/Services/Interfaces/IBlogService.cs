using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetFullDataByIdAsync(int id);
        Task<int> GetCountAsync();
        Task<IEnumerable<Blog>> GetPaginatedDatas(int page, int take);
    }
}
