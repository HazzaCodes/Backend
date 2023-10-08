
global using Backend.Data;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using Backend.UserService;
global using Microsoft.AspNetCore.Mvc;
global using Models;
global using Backend.Models;
global using System.Security.Claims;
global using Backend.Dtos;
global using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Backend.UserService.CommentService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    { c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
        Description = """ Standard Authroization header using the Bearer scheme. Example: bearer {token}" """,
        In = ParameterLocation.Header,
        Name =  "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
        c.OperationFilter<SecurityRequirementsOperationFilter>();
    });

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IlikeService, LikeService>();



builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSetting:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
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
