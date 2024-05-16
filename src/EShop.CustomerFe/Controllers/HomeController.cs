using EShop.CustomerFe.Models;
using EShop.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EShop.CustomerFe.Controllers
{
    public class HomeController : Controller
    {
        Uri uri = new Uri("https://localhost:7045/api");
        private readonly HttpClient _client;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = uri;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<CategoryResponse>? categoryList = new List<CategoryResponse>();
            HttpResponseMessage result = _client.GetAsync(_client.BaseAddress + "/v1/category/all").Result;
            if (result.IsSuccessStatusCode)
            {
                string data = result.Content.ReadAsStringAsync().Result;
                categoryList = JsonConvert.DeserializeObject<List<CategoryResponse>>(data);
            }
            return View(categoryList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
