using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DiscountDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.UseMigration();

app.Run();
