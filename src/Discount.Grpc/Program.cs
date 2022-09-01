using Discount.Grpc.Extensions;
using Discount.Grpc.Services;
using Discount.Grpc.Models;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.micros
//
//
// oft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.MigrateDatabase<Program>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddSingleton<IDatabaseSettings, DatabaseSettings>(_ =>
              builder.Configuration
                  .GetSection(nameof(DatabaseSettings))
                  .Get<DatabaseSettings>());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
