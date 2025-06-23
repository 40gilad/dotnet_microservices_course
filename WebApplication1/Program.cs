using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.SyncDataServices.Http;
using Microsoft.Extensions.Configuration;
using WebApplication1.AsyncDataServices;
using WebApplication1.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Http;
using System.IO;


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
builder.Services.AddGrpc();
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
app.MapGrpcService<GrpcPlatformService>();

app.MapGet("/protos/platforms.proto", async context =>
{
	await context.Response.WriteAsync(File.ReadAllText("protos/platforms.proto"));
});

PrepDb.PrepPopulation(app,builder.Environment.IsProduction());

app.Run();
