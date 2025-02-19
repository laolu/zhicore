using Microsoft.OpenApi.Models;
using ZhiCore.API.Middlewares;
using ZhiCore.Application.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZhiCore API", Version = "v1" });
});

// Add distributed cache
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();

// Add CAP service
builder.Services.AddCap(x =>
{
    x.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"));
    x.UseRabbitMQ("localhost");
    x.FailedRetryCount = 5;
    x.FailedThresholdCallback = failed =>
    {
        var logger = failed.ServiceProvider.GetService<ILogger<Program>>();
        logger?.LogError($"A message of type {failed.MessageType} failed after {x.FailedRetryCount} retries.");
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add global exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();