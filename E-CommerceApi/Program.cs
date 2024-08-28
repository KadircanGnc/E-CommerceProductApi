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
using E_CommerceApi.Middlewares;

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



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {    
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,                        
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(context.Exception, "JWT Authentication failed.");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("JWT Token validated succesfully.");
                return Task.CompletedTask;
            }
        };
    });

// Configure JWT authentication
//builder.Services.AddAuthorization();
//Repos
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<BrandRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderProductRepository>();
builder.Services.AddScoped<CartRepository>();

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
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<PaginationService>();
builder.Services.AddDbContext<ECommerceDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("ECommerceDb")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();


builder.Logging.AddConsole();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Date time config for postresql

/*
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}*/
//Middleware
app.UseMiddleware<ExceptionHandler>();

app.UseRouting();
// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();
//Data Seed
app.Seed();

app.MapControllers();
app.Run();
