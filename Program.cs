using System;
using ReStyleUp.Data;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Mappings;
using AutoMapper;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

//config automapper 
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ReStyle-Up API",
        Description = "API pour la gestion des images, utilisateurs, articles..."
    });

    options.SupportNonNullableReferenceTypes();
    options.EnableAnnotations(); // Pour que [SwaggerOperation] fonctionne
    options.OperationFilter<SwaggerFileOperationFilter>(); // Pour gérer les fichiers
});




builder.Services.AddScoped<IAnnonceService, AnnonceService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommandeService, CommandeService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();





var app = builder.Build();

app.UseStaticFiles();

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
