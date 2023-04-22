using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Basic Rate Limit
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("Basic", _options =>
//    {
//        _options.Window = TimeSpan.FromSeconds(10);
//        _options.PermitLimit = 2;
//        _options.QueueLimit = 2;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion


var app = builder.Build();

app.UseRateLimiter();

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
