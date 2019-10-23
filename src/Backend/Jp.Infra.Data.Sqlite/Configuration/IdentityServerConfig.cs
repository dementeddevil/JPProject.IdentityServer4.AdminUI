﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using IdentityServer4.EntityFramework.Options;
using Jp.Infra.Data.Context;

namespace Jp.Infra.Data.Sqlite.Configuration
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddIdentitySqlite(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(DatabaseConfig).GetTypeInfo().Assembly.GetName().Name;

            var operationalStoreOptions = new OperationalStoreOptions();
            services.AddSingleton(operationalStoreOptions);

            var storeOptions = new ConfigurationStoreOptions();
            services.AddSingleton(storeOptions);

            services.AddDbContext<JpContext>(options => options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<EventStoreContext>(options => options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));


            return services;
        }

    }
}