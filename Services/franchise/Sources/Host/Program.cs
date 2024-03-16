using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Host.Core;
using Host.Order;
using Host.DataBase;
using Host.Models;
using Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FranchiseDbContext>(options =>
{
    options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection"));
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.MapControllers();

app.Run();