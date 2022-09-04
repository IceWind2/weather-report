using WeatherReport.Core;
using WeatherReport.Core.WeatherDataRequest;
using WeatherReport.Core.WeatherDataRequest.RequestBuilders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRequestBuilder, OpenmeteoRequestBuilder>();
builder.Services.AddScoped<DataParser>();

var app = builder.Build();

app.MapGet("/", async (context) =>
{
    var parser = context.RequestServices.GetService<DataParser>();

    context.Response.ContentType = "application/json; charset=utf-8";
    
    await context.Response.WriteAsync(parser.Parse());
});

app.Run();