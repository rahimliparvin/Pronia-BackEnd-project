using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Services;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;

        public BrandViewComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _brandService.GetAllAsync()));
        }
    }
}

