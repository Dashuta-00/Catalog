using System.Text.Json;
using Basket.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

app.Run();
