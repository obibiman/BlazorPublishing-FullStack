using Blazor.SankoreAPI.Configurations;
using Blazor.SankoreAPI.Database;
using Blazor.SankoreAPI.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add serilog
builder.Host.UseSerilog((contxt, loggingConfig) => loggingConfig.WriteTo.Console().ReadFrom.Configuration(contxt.Configuration));

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
    y => y.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
    });

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

//add automapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

var connString = builder.Configuration.GetConnectionString("BookRepoDb");
builder.Services.AddDbContext<BookRepoContext>(options => options.UseSqlServer(connString));


//add identity
builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookRepoContext>();

builder.Services.AddControllers();  //this was apprently missed

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication(); //must come before authentication
app.UseAuthorization();
app.MapControllers();

app.Run();
