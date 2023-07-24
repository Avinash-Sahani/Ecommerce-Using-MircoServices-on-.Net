
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Catalog.API.Localization;
using Discount.Grpc.Protos;
using EventBus.Messages.Common;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddRedisService();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    port => port.Address = new Uri(configuration["GrpcSettings:DiscountUrl"] ?? string.Empty)
);
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddMassTransit(config => {
    

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ConfigureEndpoints(ctx);
    });
});

/*builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
       // cfg.Host(configuration["EventBusSettings:HostAddress"]); // Replace with your RabbitMQ host URL
        
     //   cfg.ConfigureEndpoints(context);
    });
});*/

//builder.Services.AddMassTransitHostedService();
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