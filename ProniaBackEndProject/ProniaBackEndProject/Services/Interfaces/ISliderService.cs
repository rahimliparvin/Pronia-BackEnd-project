using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAll();
      
    }
}
