using EShop.CustomerFe.Services;
using EShop.CustomerFe.Services.Implement;
using EShop.CustomerFe.Services.Implements;
using EShop.CustomerFe.Services.Interface;
using EShop.CustomerFe.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/AccessDenied";
});

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Sent every request to the API with the token
//var configureClient = new Action<IServiceProvider, HttpClient>(async (provider, client) =>
//{
//    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
//    var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

//    client.BaseAddress = new Uri("https://localhost:7045/api");
//    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
//});
//Add Interface and Implement
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICategoryService, CategoryService>();
builder.Services.AddHttpClient<IProductReviewService, ProductReviewService>();
builder.Services.AddHttpClient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
