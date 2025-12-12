using BelbimEShop.Identity.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "belbim.server",
            ValidAudience = "belbim.client",
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this-is-a-secret-key-for-jwt-token-generation"))
        };
    });


//Diðer microservice'ler, aþaðýdaki gibi token onaylama yapýsýný kendi içeriklerine göre uyarlamalýdýr.
//Yani doðrudan identity microservice'ine istek atýp token doðrulamasý yapmamalýdýrlar.
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    option.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
