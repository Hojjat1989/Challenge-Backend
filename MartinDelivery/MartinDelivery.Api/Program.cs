using System;
using MartinDelivery.Api;
using MartinDelivery.Api.Containers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connections = builder.Configuration.GetSection(nameof(ConnectionStrings));
builder.Services.Register(connections);

var app = builder.Build();
app.MapControllers();

app.Run();