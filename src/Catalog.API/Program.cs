using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDatabaseSettings, DatabaseSettings>(_ =>
              builder.Configuration
                  .GetSection(nameof(DatabaseSettings))
                  .Get<DatabaseSettings>());
builder.Services.AddTransient<ICatalogContext, CatalogContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();


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
