using EShop.Core.Domain.Entities;
using EShop.Core.Domain.Repositories;
using EShop.Core.IServices;
using EShop.Core.Services;
using EShop.Infrastucture.Data;
using EShop.Infrastucture.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace EShop.API.Extensions
{
    public static class ConfigureExtension
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectDB = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(connectDB));
            services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;

        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var startTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")).ToString();

                var swaggerDescription = "## Description. \n\n" +
                "- This is a list of APIs we use to manage the E commerce Application. \n\n" +
                "\n\n" +
                $"* Last updated at: __{startTime}__ \n\n";

                OpenApiInfo apiInfo = new OpenApiInfo
                {
                    Title = "Ecommerce Swagger UI",
                    Description = swaggerDescription,
                    Version = "development"
                };
                options.SwaggerDoc("v1", apiInfo);
                //string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

    }
}
