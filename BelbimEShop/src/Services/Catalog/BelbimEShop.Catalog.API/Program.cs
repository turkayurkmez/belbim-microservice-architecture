using BelbimEShop.Catalog.Application.Contracts;
using BelbimEShop.Catalog.Application.Features.Product.Commands.DiscountProductPrice;
using BelbimEShop.Catalog.Domain.Repositories;
using BelbimEShop.Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DiscountProductPriceCommandRequestHandler>();
builder.Services.AddScoped<IProductRepository, FakeProductRepository>();

builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssemblyContaining<DiscountProductPriceCommandRequest>();
    config.RegisterServicesFromAssemblyContaining<IRegisterMarker>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
