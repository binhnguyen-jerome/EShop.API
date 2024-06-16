using EShop.API.Extensions;
using EShop.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Inject Service
builder.Services.ConfigureService(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.UseMiddleware<ExceptionHandlingMiddleWare>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
