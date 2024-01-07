using Microsoft.EntityFrameworkCore;
using productManagement.api.input.Configurations;
using productManagement.application.input.seedWork.repository;
using productManagement.application.input.services.product;
using productManagement.application.input.services.product.interfaces;
using productManagement.application.input.services.supplier;
using productManagement.application.input.services.supplier.interfaces;
using productManagement.domain.aggregates.supplier;
using productManagement.infra.data.input;
using productManagement.infra.data.input.aggregates;
using ProductManagement.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAndConfigureControllers();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddDbContext<ContextProductManagement>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContextProductManagement")));
builder.Services.AddScoped<IDbContext, UnitOfWork>();
builder.Services.AddScoped<ICreateSupplierService, CreateSupplierService>();
builder.Services.AddScoped<IModifySupplierService, ModifySupplierService>();
builder.Services.AddScoped<ISupplierAppRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ICreateProductService, CreateProductService>();
builder.Services.AddScoped<IModifyProductService, ModifyProductService>();
builder.Services.AddScoped<IProductAppRepository, ProductRepository>();
builder.Services.AddScoped<IInactivateProductService, InactivateProductService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //replace DataContext with your Db Context name
    var dataContext = scope.ServiceProvider.GetRequiredService<ContextProductManagement>();
    dataContext.Database.Migrate();
}

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
