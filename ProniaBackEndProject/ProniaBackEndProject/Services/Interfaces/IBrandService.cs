using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brands>> GetAllAsync();
        Task<Brands> GetByIdAsync(int id);
    }
}
