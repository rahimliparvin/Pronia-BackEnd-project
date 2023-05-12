using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllAsync();
        Task<Color> GetByIdAsync(int id);
    }
}
