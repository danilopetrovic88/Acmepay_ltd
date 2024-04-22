using Acmepay_ltd.Endpoints;
using Acmepay_ltd.Models;
using Acmepay_ltd.Services;
using Dapper;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                           throw new ApplicationException("Connection string is null");

    return new SqlConnectionFactory(connectionString);

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTransactionEndpoints();

app.Run();
