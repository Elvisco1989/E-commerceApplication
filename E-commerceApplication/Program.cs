using E_commerceApplication;
using E_commerceApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "AllowAll",
              builder =>
              {
                  builder.AllowAnyOrigin();
                  builder.AllowAnyMethod();
                  builder.AllowAnyHeader();
              });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Secretkey").Get<string>();
builder.Services.AddSingleton<PaymentService>();
builder.Services.AddSingleton<FoodRepository>();
builder.Services.AddScoped<EcommerceDBRepositoryClass>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DockerConnection2")));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "E_commerceApplication", Version = "v1" });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "img")),
    RequestPath = "/img"
});
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
