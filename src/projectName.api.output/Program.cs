using Microsoft.EntityFrameworkCore;
using projectName.application.output.interfaces;
using projectName.infra.data.output;
using projectName.infra.data.output.repositories;
using ProductManagement.Api.Output.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAndConfigureControllers();
//builder.Services.AddDbContext<ContextProductManagement>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContextProductManagement")));

builder.Services.AddScoped<SqlFactory>();
builder.Services.AddScoped<IReadProductRepository, ProductRepository>();

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
