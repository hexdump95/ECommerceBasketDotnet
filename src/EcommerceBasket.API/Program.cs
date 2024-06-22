using EcommerceBasket.Application.Services;
using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Repositories;
using EcommerceBasket.Infrastructure.Configuration;
using EcommerceBasket.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection")!;
builder.Services.AddSingleton(new RedisConfiguration(redisConnectionString));
// builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
//     ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!));

builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
