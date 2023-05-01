using CodingTest.REST.Services;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodingTest.API", Version = "v1" }); });

builder.Services.AddScoped<IEmailExtractService, EmailExtractService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodingTest.API v1"));
}

app.MapControllers();

app.Run();
