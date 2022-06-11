using Blazor.SankoreAPI.Configurations;
using Blazor.SankoreAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add serilog
builder.Host.UseSerilog((contxt,loggingConfig)=>loggingConfig.WriteTo.Console().ReadFrom.Configuration(contxt.Configuration));

builder.Services.AddCors(options => { options.AddPolicy("AllowAll",
y => y.AllowAnyMethod()
.AllowAnyHeader()
    .AllowAnyOrigin()); });

//add automapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

var connString =  builder.Configuration.GetConnectionString("BookRepoDb");
builder.Services.AddDbContext<BookRepoContext>(options => options.UseSqlServer(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();