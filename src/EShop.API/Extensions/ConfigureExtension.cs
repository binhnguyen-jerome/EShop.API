﻿using EShop.Application.Services.Implements;
using EShop.Application.Services.Interfaces;
using EShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using EShop.Core.Entities;
using EShop.Core.Repositories;
using EShop.Infrastructure.Repositories;

namespace EShop.API.Extensions
{
    public static class ConfigureExtension
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            //Inject Query 
            services.AddScoped<IProductQueries, ProductQueries>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IProductReviewQueries, ProductReviewQueries>();
            services.AddScoped<ICartQueries, CartQueries>();
            // Inject Service Repositories
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            // Connect to Database
            var connectDb = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(connectDb));
            // Add Identity
            services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            // Check password IdentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            // Add JWT
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException()))
                    };
                });

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

                var apiInfo = new OpenApiInfo
                {
                    Title = "Ecommerce Swagger UI",
                    Description = swaggerDescription,
                    Version = "development",
                    TermsOfService = null,
                    Contact = null,
                    License = null,
                    Extensions = null
                };
                options.SwaggerDoc("v1", apiInfo);
                // Add JWT bearer token support
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddCors(options =>
            {
                options.AddPolicy(myAllowSpecificOrigins,
                                       policy =>
                                       {
                                           policy
                                           .AllowAnyOrigin()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                                       });
            });
            return services;
        }
    }
}
