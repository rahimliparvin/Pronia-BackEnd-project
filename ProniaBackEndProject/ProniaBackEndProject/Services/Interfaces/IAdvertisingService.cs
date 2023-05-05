using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IAdvertisingService
    {
        Task<IEnumerable<Advertising>> GetAllAsync();

    }
}
