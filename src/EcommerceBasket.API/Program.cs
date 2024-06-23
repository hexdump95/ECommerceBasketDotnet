using EcommerceBasket.API.Configuration;
using EcommerceBasket.Application.Services;
using EcommerceBasket.Application.Services.Interfaces;
using EcommerceBasket.Domain.Repositories;
using EcommerceBasket.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureQuartz();
builder.ConfigureRedis();

builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));

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
