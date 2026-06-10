using Recetario_Api;
using Microsoft.AspNetCore.Hosting;
using ejemplo;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);
app.Run();
