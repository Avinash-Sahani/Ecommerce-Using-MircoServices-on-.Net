
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

var builder = WebApplication.CreateBuilder(args);
var currentEnvironmentName =  builder.Environment.EnvironmentName;;
builder.Configuration.AddJsonFile($"ocelot.{currentEnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOcelot().AddCacheManager(settings => { settings.WithDictionaryHandle();});
var app = builder.Build();




app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();