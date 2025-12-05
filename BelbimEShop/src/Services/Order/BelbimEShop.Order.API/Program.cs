using BelbimEShop.Order.API.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
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
      
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("order-service-discount-queue", e =>
        {
            //2. Eklediðiniz tüketiciyi burada yapýlandýrýn

            e.ConfigureConsumer<OrderProductPriceDiscountConsumer>(context);
        });

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
