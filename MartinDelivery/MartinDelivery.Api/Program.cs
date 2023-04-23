using System;
using MartinDelivery.Api;
using MartinDelivery.Api.Containers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var connectionStrings = builder.Configuration.GetSection(nameof(ConnectionStrings));
builder.Services.Register(connectionStrings);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();