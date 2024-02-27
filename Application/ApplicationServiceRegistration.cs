using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Serilog;
using Core.CrossCuttingConcerns.Serilog.Logger;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    //extension for IServiceCollection to add all the services in the Application project
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //add all the services in the Application project
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.Where(type =>
            {
                var name = type.Name;
                return name.EndsWith("BusinessRules");
            }))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.Where(type =>
            {
                var name = type.Name;
                return name.EndsWith("Manager");
            }))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(
            configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
                configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
                configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            });

        services.AddSingleton<LoggerServiceBase, FileLogger>();
        //services.AddSingleton<LoggerServiceBase, MsSqlLogger>();

        services.AddDistributedMemoryCache();
        /*services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "192.168.33.129:6379";
            options.InstanceName = "SampleInstance";
        });*/
        services.AddSingleton(typeof(Random));

        return services;
    }
}

//var res = services.FirstOrDefault(s => s.ImplementationType == typeof(BudgetManager));
//var re2s = services.FirstOrDefault(s => s.ImplementationType == typeof(CategoryManager));