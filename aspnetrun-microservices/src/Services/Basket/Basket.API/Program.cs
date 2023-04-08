
using Catalog.API.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddRedisService();

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
        options.Configuration = GetConnectionString(); // Replace with your Redis server details// Replace with a unique name for your application

        string? GetConnectionString()
        {
           return builder.Configuration.GetValue<string>(Localizable.ConnectionString);
        }
    });
}

#endregion 