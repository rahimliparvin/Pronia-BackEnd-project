using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetFullDataByIdAsync(int id);
    }
}
