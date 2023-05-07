using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface ISizeService
    {
        Task<IEnumerable<Size>> GetAllAsync();

    }
}
