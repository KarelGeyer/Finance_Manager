using System;
using CategoryService.Service;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Supabase configuration
var configuration = builder.Configuration;
var supabaseUrl = configuration["SupaBase:Url"];
var supabaseKey = configuration["SupaBase:Key"];
builder.Services.AddSingleton(_ =>
{
    var options = new Supabase.SupabaseOptions { AutoConnectRealtime = true };

    var client = new Supabase.Client(supabaseUrl!, supabaseKey, options);
    client.InitializeAsync().Wait();

    return client;
});

// Dependency injection
builder.Services.AddScoped(typeof(IDbService<>), typeof(DbService<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
