using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.ViewComponents
{
    public class AdvertisingViewComponent : ViewComponent
    {
        private readonly IAdvertisingService _advertisingService;
        public AdvertisingViewComponent(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _advertisingService.GetAllAsync()));
        }
    }
}
