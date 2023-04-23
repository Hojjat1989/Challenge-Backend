using System;
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

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped(typeof(IOrderService), typeof(OrderService));
    }
}
