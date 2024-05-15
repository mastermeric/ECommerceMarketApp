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

// builder.Services.AddRateLimiter(
//     options =>   {
//         options.AddPolicy("User", cntx => 
//             RateLimitPartition.GetFixedWindowLimiter(
//                 cntx.Request.Headers.Host.ToString(),
//                 partition => new FixedWindowRateLimiterOptions {
//                     AutoReplenishment = true ,
//                     PermitLimit = 10,
//                     Window = TimeSpan.FromMinutes(1)
//                 }));
                
//                 options.AddPolicy("Auth", httpContext =>
//                 RateLimitPartition.GetFixedWindowLimiter(httpContext.Request.Headers.Host.ToString(),
//                 partition => new FixedWindowRateLimiterOptions
//                 {
//                     AutoReplenishment = true,
//                     PermitLimit = 5,
//                     Window = TimeSpan.FromMinutes(1)
//                 }));

//                 options.OnRejected = async (context, token) =>
//                 {
//                     context.HttpContext.Response.StatusCode = 429;

//                     if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
//                     {
//                         await context.HttpContext.Response.WriteAsync(
//                             $"İstek sınır sayısına ulaştınız. {retryAfter.TotalMinutes} dakika sonra tekrar deneyiniz. ", cancellationToken: token);
//                     }
//                     else
//                     {
//                         await context.HttpContext.Response.WriteAsync(
//                             "İstek sınırına ulaştınız. Daha sonra tekrar deneyin. ", cancellationToken: token);
//                     }
//                 };                
//     }
// );

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
