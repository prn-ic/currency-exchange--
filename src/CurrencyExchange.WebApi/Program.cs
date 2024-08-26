using System.Reflection;
using System.Text.Json.Serialization;
using CurrencyExchange.Core.Currencies;
using CurrencyExchange.Core.Wallets;
using CurrencyExchange.Persistanse.Data;
using CurrencyExchange.Persistanse.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull
    );

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseNpgsql(
        builder.Configuration.GetConnectionString("psql"),
        b => b.MigrationsAssembly("CurrencyExchange.WebApi")
    );
});
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ICurrencyMarketRepository, CurrencyMarketRepository>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<ICurrencyMarketService, CurrencyMarketService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.Load("CurrencyExchange.Persistanse"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
