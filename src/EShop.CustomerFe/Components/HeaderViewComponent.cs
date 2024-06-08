using EShop.CustomerFe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoryClientService categoryService;

        public HeaderViewComponent(ICategoryClientService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
    }
}
