using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
    }
}
