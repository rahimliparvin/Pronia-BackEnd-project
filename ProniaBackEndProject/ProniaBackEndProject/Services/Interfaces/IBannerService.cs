using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface IBannerService
    {
       Task<IEnumerable<Banner>> GetAllAsync();


       Task<Banner> GetByIdAsync(int id);
    }
}
