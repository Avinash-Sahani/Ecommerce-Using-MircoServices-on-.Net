
using Basket.API.Repositories;
using Catalog.API.Localization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddRedisService();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Helpers

void AddRedisService()
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = configuration[Localizable.CacheUrl]; 
        options.InstanceName = "Basket";
        
    });
}

#endregion 