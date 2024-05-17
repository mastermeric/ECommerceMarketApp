using System.Threading.RateLimiting;
using ECommerceMarketApp.DataContext;
using ECommerceMarketApp.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//FLUTTER UI 3000  den haberlesecek.
// builder.Services.AddCors(opt =>
//             opt.AddPolicy(name: "ApiCorsPolicy", builder =>
//             builder.WithOrigins("http://localhost:53739")
//             .AllowAnyMethod()
//             .AllowAnyHeader()
//             .AllowCredentials()));

builder.Services.AddCors(options =>
    {
        options.AddPolicy("ApiCorsPolicy", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });            



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("ApiCorsPolicy"); //call UseCors before the UseAuthorization, UseEndpoints

// Image file kullanimi icin..
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
