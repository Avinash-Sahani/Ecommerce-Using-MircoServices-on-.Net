using Shopping.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;
builder.Services.AddHttpClient<ICatalogService,CatalogService>(service =>
    service.BaseAddress = new Uri(configuration["ApiSettings:CatalogUrl"] ?? string.Empty));
builder.Services.AddHttpClient<IBasketService,BasketService>(service =>
    service.BaseAddress = new Uri(configuration["ApiSettings:BasketUrl"] ?? string.Empty));
builder.Services.AddHttpClient<IOrderService,OrderService>(service =>
    service.BaseAddress = new Uri(configuration["ApiSettings:OrderingUrl"] ?? string.Empty));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
