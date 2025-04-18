using System;
using ReStyleUp.Data;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Mappings;
using AutoMapper;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.Services;


var builder = WebApplication.CreateBuilder(args);

//config automapper 
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAnnonceService, AnnonceService>();


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
