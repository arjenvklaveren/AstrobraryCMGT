using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddScoped<ISpaceBodyRepository, SpaceBodyRepository>();
builder.Services.AddScoped<ISpaceBodyService, SpaceBodyService>();

builder.Services.AddScoped<IAstronomerRepository, AstronomerRepository>();
builder.Services.AddScoped<IAstronomerService, AstronomerService>();

builder.Services.AddScoped<IObjectImageService, ObjectImageService>();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://192.168.2.21:4200"));

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
