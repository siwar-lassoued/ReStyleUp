using ReStyleUp;
using System;
using ReStyleUp.Data;
using ReStyleUp.Models;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Mappings;
using AutoMapper;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Configuration de la connexion à la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configuration de JWT
var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
var jwtSettings = builder.Configuration.GetSection("JwtBearerTokenSettings").Get<JwtBearerTokenSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});
//config automapper 
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);




// Add services to the container.

builder.Services.AddControllers();
// Configure Swagger
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



// Services
builder.Services.AddScoped<IAnnonceService, AnnonceService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommandeService, CommandeService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200") // adresse du frontend
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAngularApp");

app.UseAuthentication(); // Authentification d'abord
app.UseAuthorization();  // Ensuite l'autorisation

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();


app.MapControllers();

app.Run();
