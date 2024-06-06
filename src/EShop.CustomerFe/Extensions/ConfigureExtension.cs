using EShop.CustomerFe.Services;
using EShop.CustomerFe.Services.Implement;
using EShop.CustomerFe.Services.Implements;
using EShop.CustomerFe.Services.Interface;
using EShop.CustomerFe.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace EShop.CustomerFe.Extensions
{
    public static class ConfigureExtension
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/User/AccessDenied";
            });
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Sent every request to the API with the token
            var configureClient = new Action<IServiceProvider, HttpClient>(async (provider, client) =>
            {
                var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                var claimsPrincipal = httpContextAccessor.HttpContext?.User;

                if (claimsPrincipal != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var accessToken = identity.FindFirst("token")?.Value;

                    client.BaseAddress = new Uri(configuration["HttpClientSettings:BaseAddress"]);
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                }
            });
            services.AddScoped<ICacheClientService, CacheClientService>();
            //Inject Service Repositories
            services.AddHttpClient<IProductClientService, ProductClientService>(configureClient);
            services.AddHttpClient<ICategoryClientService, CategoryClientService>(configureClient);
            services.AddHttpClient<IProductReviewClientService, ProductReviewClientService>(configureClient);
            services.AddHttpClient<IUserClientService, UserClientService>(configureClient);
            services.AddHttpClient<ICartClientService, CartClientService>(configureClient);
            services.AddHttpClient<IAuthClientService, AuthClientService>(configureClient);
            return services;
        }
    }
}
