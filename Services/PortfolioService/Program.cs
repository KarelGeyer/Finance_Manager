using DbService;
using Microsoft.EntityFrameworkCore;
using PortfolioService.Helpers;
using PortfolioService.Interfaces;
using PortfolioService.Interfaces.Services;
using PortfolioService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped(typeof(IDbService<>), typeof(DbService<>));
builder.Services.AddScoped(typeof(IPortfolioCommonService<>), typeof(PortfolioCommonService<>));
builder.Services.AddScoped(typeof(ICommonService<>), typeof(CommonService<>));
builder.Services.AddScoped<ILoansService, LoansService>();
builder.Services.AddTransient(typeof(IValidation<>), typeof(Validation<>));
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
