using BusinessLogic.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using E_CommerceApi;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Validators;
using E_CommerceApi.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using System;
using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Authentication.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
//Swagger JWT configuration
builder.Services.AddSwaggerGen(c =>
{
    // Add the "Authorization" header with a "Bearer" scheme to the Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});

//Repos
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<BrandRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderProductRepository>();

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();

//Services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderProductService>();
builder.Services.AddDbContext<ECommerceDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("ECommerceDb")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Date time config for postresql

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseRouting();
// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
//Data Seed
app.Seed();

app.MapControllers();
app.Run();
