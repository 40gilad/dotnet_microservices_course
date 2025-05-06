using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.SyncDataServices.Http;
using Microsoft.Extensions.Configuration;
using WebApplication1.AsyncDataServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
if(builder.Environment.IsProduction())
{
	Console.WriteLine("--> Using MSSQL Db");
	builder.Services.AddDbContext<AppDbContext>(options => 
		options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
	Console.WriteLine("--> Using InMem Db");
	builder.Services.AddDbContext<AppDbContext>(options => 
		options.UseInMemoryDatabase("InMem"));
}
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddHttpClient<ICommandDataClient, CommandDataClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

Console.WriteLine($"-->CommandService Endpoint: {builder.Configuration["CommandService"]}");

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app,builder.Environment.IsProduction());

app.Run();
