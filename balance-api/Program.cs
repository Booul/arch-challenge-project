using Microsoft.Extensions.Options;
using BalanceApi.Business;
using BalanceApi.Business.Interfaces;
using BalanceApi.Models;
using BalanceApi.Models.Interfaces;
using BalanceApi.Repositories;
using BalanceApi.Repositories.Abstracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection Dependency
builder.Services.AddScoped<IBalanceBusiness, BalanceBusiness>();
builder.Services.AddScoped<IRedisCacheBusiness, RedisCacheBusiness>();
builder.Services.AddScoped<IRedisCacheRepository, RedisCacheRepository>();
builder.Services.AddScoped<IInternalMemoryCacheRepository, InternalMemoryCacheRepository>();
builder.Services.AddScoped<BalanceApi.Repositories.ILogger, ConsoleLoggerRepository>();

// MongoDB
builder.Services.Configure<ArchChallengeProjectDatabaseSettings>(
    builder.Configuration.GetSection(nameof(ArchChallengeProjectDatabaseSettings))
);

builder.Services.AddSingleton<IMongoDbDatabaseSettings>(
    sp => sp.GetRequiredService<IOptions<ArchChallengeProjectDatabaseSettings>>().Value
);

builder.Services.AddScoped<MongoDbRepository<Transaction>, BalanceRepository>();

// Redis
builder.Services.AddSession();

// Internal Memory
builder.Services.AddMemoryCache();

builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration =
        builder.Configuration.GetConnectionString("RedisConnectionString");
});

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
