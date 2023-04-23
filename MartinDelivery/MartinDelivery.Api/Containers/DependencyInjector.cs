using System;
using Hangfire;
using Hangfire.MemoryStorage;
using MartinDelivery.Api.Webhooks;
using MartinDelivery.Application.Interfaces;
using MartinDelivery.Core.Base;
using MartinDelivery.Infrastructure;
using MartinDelivery.Services;
using Microsoft.EntityFrameworkCore;

namespace MartinDelivery.Api.Containers;

public static class DependencyInjector
{
    private const string MartinDbName = "MartinDb";

    public static void Register(this IServiceCollection services, IConfigurationSection config)
    {
        services.AddDbContext<MartinDeliveryDbContext>(options =>
        {
            options.UseSqlServer(config.GetValue<string>(MartinDbName));
        }, ServiceLifetime.Scoped);

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseMemoryStorage());
        services.AddHangfireServer();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped(typeof(IOrderService), typeof(OrderService));
        services.AddScoped(typeof(IWebhookService), typeof(WebhookService));

        services.AddScoped(typeof(IWebhookPublisher), typeof(WebhookPublisher));
    }
}
