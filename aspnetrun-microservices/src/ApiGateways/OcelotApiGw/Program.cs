using Microsoft.Extensions.Hosting.Internal;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

var builder = WebApplication.CreateBuilder(args);
var currentEnvironmentName =  builder.Environment.EnvironmentName;;
builder.Configuration.AddJsonFile($"ocelot.{currentEnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();


app.MapGet("/", () => "Hello World!");

app.Run();