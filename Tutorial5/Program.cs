//using Tutorial5.Services;

using Microsoft.EntityFrameworkCore;
using Tutorial5.Models;
using Tutorial5.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IClientService, ClientService>();


builder.Services.AddDbContext<TravelAgencyContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseAuthorization();
app.MapControllers();
app.Run();