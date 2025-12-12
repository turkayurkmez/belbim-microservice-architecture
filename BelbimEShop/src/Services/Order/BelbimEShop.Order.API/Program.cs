using BelbimEShop.Order.API.Consumers;
using BelbimEShop.Shared.EventBus;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{


    configurator.AddConsumer<StockUnavailableConsumer>();
    configurator.AddConsumer<PaymentSuccessfulConsumer>();
    configurator.AddConsumer<PaymentFailedConsumer>();
    //1. Tüketiciyi (consumer) ekleyin
    configurator.AddConsumer<OrderProductPriceDiscountConsumer>(c => {
        //Tüketiciye özel yapýlandýrmalar burada yapýlabilir
        //retry pattern:
        c.UseMessageRetry(r =>
        {
            r.Intervals(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15)); //3 kez, 5 saniye arayla yeniden dene
            //r.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)); //exponential backoff
            r.Handle<TimeoutException>(); //Sadece TimeoutException durumunda yeniden dene();
            //r.ConnectRetryObserver(new OrderRetryObserver(builder.Logging.CreateLogger<OrderRetryObserver>())); //Özel retry gözlemcisi ekle
        });

        //Circuit Breaker pattern:
        c.UseCircuitBreaker(cb =>
        {
            cb.TrackingPeriod = TimeSpan.FromMinutes(1); //1 dakika boyunca baþarýsýzlýklarý takip et
            cb.TripThreshold = 5; //5 kez baþarýsýz olursa devre kesilsin
            cb.ActiveThreshold = 10; //Devre kesici aktif olmasý için en az 10 istek olmalý
            cb.ResetInterval = TimeSpan.FromMinutes(5); //Devre kesildikten sonra 5 dakika sonra tekrar denesin
        });

    });



    configurator.UsingRabbitMq((context, config) =>
    {
      
        config.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("order-service-discount-queue", e =>
        {
            //2. Eklediðiniz tüketiciyi burada yapýlandýrýn

            e.ConfigureConsumer<OrderProductPriceDiscountConsumer>(context);
        });

        //BUNU UNUTTUÐUM ÝÇÝN EN AZ 20 DK KAYBETTÝM!!!
        config.ConfigureEndpoints(context);

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapPost("/createOrder", async (IPublishEndpoint publishEndpoint, CreateOrderRequest request) =>
{

    var orderItems = request.OrderItems.Select(o => new OrderItemInEvent(o.ProductId, o.Quantity, o.Price));

    var orderId = new Random().Next(1000, 10000);

    var orderCreatedEvent = new OrderCreatedEvent(orderId, request.CustomerId, orderItems.ToList(), "1111111111");

    await publishEndpoint.Publish(orderCreatedEvent);

    return Results.Ok(new { message = $"{orderId} numaralý sipariþ baþarýlý" });


});



app.Run();


public record OrderItem(long ProductId, int Quantity, decimal Price);

public record CreateOrderRequest(string CustomerId, List<OrderItem> OrderItems);
