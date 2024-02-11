﻿
using Application.Services.Repositories;
using Core.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("RentACar"));
        });

        services.AddIdentityCore<AppUser>(options =>
        {
        }).AddRoles<AppRole>().AddEntityFrameworkStores<BaseDbContext>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        //var res = services.FirstOrDefault(s => s.ImplementationType == typeof(RefreshTokenRepository));

    }
}