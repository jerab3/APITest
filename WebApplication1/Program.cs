using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrganizationStoreManagement"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyWebApp",
        builder =>
        {
            builder.WithOrigins("https://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowMyWebApp");

app.MapControllers();

app.Run();  
