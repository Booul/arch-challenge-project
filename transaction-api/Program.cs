using Microsoft.Extensions.Options;
using TransactionApi.Models.Interfaces;
using TransactionApi.Repositories;
using TransactionApi.Repositories.Abstracts;
using TransactionApi.Models;
using TransactionApi.Business;
using TransactionApi.Business.Interfaces;
using TransactionApi.Data.Converter.Contract;
using TransactionApi.Data.Converter.Implementations;
using TransactionApi.Data.VO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection Dependency
builder.Services.AddScoped<IParser<Transaction, TransactionVO>, TransactionConverter>();
builder.Services.AddScoped<IParser<TransactionVO, Transaction>, TransactionConverter>();
builder.Services.AddScoped<IBalanceBusiness<Transaction, TransactionVO>, BalanceBusiness>();
builder.Services.AddScoped<ICache, RedisCacheRepository>();
builder.Services.AddScoped<TransactionApi.Repositories.ILogger, ConsoleLoggerRepository>();

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
