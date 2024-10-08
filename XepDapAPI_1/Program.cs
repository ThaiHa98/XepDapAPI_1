﻿using Data.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using XeDapAPI.Helper;
using XeDapAPI.Repository.Interface;
using XeDapAPI.Repository.Repositorys;
using XeDapAPI.Service.Interfaces;
using XeDapAPI.Service.Services;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Repository.Repositorys;
using XepDapAPI_1.Service.Interfaces;
using XepDapAPI_1.Service.Services;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Add services to the container.
builder.Services.AddScoped<IUserIService, UserService>();
builder.Services.AddScoped<IUserInterface, UserRepository>();
builder.Services.AddScoped<Token>();
builder.Services.AddScoped<ISlideIService, SlideService>();
builder.Services.AddScoped<ISlideInterface, SlideRepository>();
builder.Services.AddScoped<ITypeIService, TypeService>();
builder.Services.AddScoped<IBrandIService, BrandService>();
builder.Services.AddScoped<IProductsIService ,ProductsService>();
builder.Services.AddScoped<IProduct_DetailIService, Product_DetailService>();
builder.Services.AddScoped<IProductsInterface, ProductsRepository>();
builder.Services.AddScoped<IProducts_DrtailInterface, Products_DetailRepository>();
builder.Services.AddScoped<ICartInterface, CartRepository>();
builder.Services.AddScoped<IOrderIService, OrderService>();
builder.Services.AddScoped<ICartIService, CartService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
    
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "XeDapAPI", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
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
            new string[] {}
        }
    });
});

builder.Services.AddSwaggerGen();
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
var token256 = builder.Configuration["Jwt:Token256"];

if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience) || string.IsNullOrWhiteSpace(token256))
{
    throw new ArgumentNullException("JWT configuration values cannot be null or empty.");
}

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token256"]))
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "authenticationToken"; //Tên của cookie
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login"; //đường dẫn đến trang đăng nhập
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .WithMethods("POST","PUT", "GET","DELETE", "OPTIONS")
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(@"D:\Thai\WebBanXeDap\XepDapAPI_1\Data", "Image")),
    RequestPath = "" // Bỏ qua đường dẫn để có thể truy cập trực tiếp
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("AllowOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
