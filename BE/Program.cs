using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using db_Api.Models.Generated;
using db_Api.Repository.ReunionsRepository;
using db_Api.Repository.EmployeeRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Définition du nom de la politique CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Création de l'application web avec les arguments fournis
var builder = WebApplication.CreateBuilder(args);

// Configuration de la politique CORS pour permettre les requêtes de toutes les origines
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

// Configuration des services de l'API
builder.Services.AddScoped<IReunionRepository, ReunionRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Configuration du contexte de base de données
builder.Services.AddDbContext<Db_PartialContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings")));

// Configuration des services MVC
builder.Services.AddControllers();

// Configuration de l'authentification JWT
var secretKey = builder.Configuration.GetSection("AppSettings:key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key
        };
    });


// Configuration de Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v2",
        Description = "API Mobile"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef.....')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Création de l'application
var app = builder.Build();

// Configuration spécifique à l'environnement de développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

// Configuration du routage
app.UseRouting();

// Configuration de la politique CORS
app.UseCors(MyAllowSpecificOrigins);

// Configuration de l'authentification et de l'autorisation
app.UseAuthentication();
app.UseAuthorization();

// Configuration des endpoints
app.MapControllers();

// Exécution de l'application
app.Run();
