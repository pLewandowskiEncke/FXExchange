using FXExchange.Infrastructure;
using FXExchange.Interfaces;
using FXExchange.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a new instance of the service collection
        var services = new ServiceCollection();

        // Add services to the collection
        services.AddTransient<IFXHandler, FXHandler>();
        services.AddTransient<IFXValidationService, FXValidationService>();
        services.AddTransient<IFXCalculationService, FXCalculationService>();
        services.AddTransient<IFXRatesRetrievalService, FXRatesRetrievalService>();
        services.AddTransient<ILogger, ConsoleLogger>();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // Resolve the service
        var handler = serviceProvider.GetService<IFXHandler>();
        if (handler == null)
        {
            throw new InvalidOperationException("Service not found.");
        }
        await handler.Handle(args);
    }
}
