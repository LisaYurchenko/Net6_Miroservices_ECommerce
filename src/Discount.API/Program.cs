using Discount.API.Models;
using Discount.API.Services;
using Discount.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddSingleton<IDatabaseSettings, DatabaseSettings>(_ =>
              builder.Configuration
                  .GetSection(nameof(DatabaseSettings))
                  .Get<DatabaseSettings>());
builder.Services.AddControllers();
builder.MigrateDatabase<Program>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
