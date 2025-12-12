using BelbimEShop.Catalog.API;
using BelbimEShop.Catalog.Application.Contracts;
using BelbimEShop.Catalog.Application.Features.Product.Commands.DiscountProductPrice;
using BelbimEShop.Catalog.Domain.Repositories;
using BelbimEShop.Catalog.Infrastructure.EventHandlers;
using BelbimEShop.Catalog.Infrastructure.Persistance;
using BelbimEShop.Catalog.Infrastructure.Repositories;
using BelbimEShop.Shared.EventBus;
using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DiscountProductPriceCommandRequestHandler>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssemblyContaining<DiscountProductPriceCommandRequest>();
    config.RegisterServicesFromAssemblyContaining<IRegisterMarker>();
    config.RegisterServicesFromAssemblyContaining<DiscountProductPriceDomainEventHandler>();
});


var connectionString = builder.Configuration.GetConnectionString("db");
connectionString = connectionString?.Replace("{{HOST}}", builder.Configuration["DefaultHost"])
                                    .Replace("{{PASS}}", builder.Configuration["DefaultPass"]);


var rabbitMqConnectionString = builder.Configuration.GetConnectionString("DefaultMQ");

builder.Services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddMassTransit(configurator =>
{

    //Outbox pattern:
    //Eðer gönderici, rabbitmq'ya mesaj gönderemezse... son olaylarý db'ye kaydetsin. Uygulama yeniden ayaða kalktýðýnda bütün kayýtlarý göndersin

    configurator.AddEntityFrameworkOutbox<CatalogDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox(); // masstransit üzerinden gönderilmeye çalýþan fakat baþarýsýz olan olaylar db'ye kaydedilir.
        o.QueryDelay = TimeSpan.FromMinutes(2);
      

    });


    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });


        config.Publish<ProductPriceDiscountedIntegrationEvent>(tc =>
        {
            tc.Durable = true; //mesaj kalýcý olsun.
            tc.AutoDelete = false; //Kuyruk otomatik silinmesin.
            tc.ExchangeType = RabbitMQ.Client.ExchangeType.Fanout;
        });
    });


});


var app = builder.Build();

//connectionString bilgisini log'da göster:
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("DÝKKATT BAÐLANTI CÜMLESÝ!!!!: {ConnectionString}", connectionString);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await DatabaseInitializer.InitializeAsync(app);

app.Run();
