using EShop.CustomerFe.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CustomerFe.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public HeaderViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            ViewBag.Token = HttpContext.Session.GetString("Token");
            return View(categories);
        }
    }
}
