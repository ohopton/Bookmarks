﻿using System;
using System.IO;
using Bookmarks.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Bookmarks.Logic.Services;
using Dapper;
using Bookmarks.Logic.TypeHandlers;

namespace Bookmarks.Api
{
    public class Startup
    {
        private IConfiguration configuration;

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            loggerFactory.AddConsole();

            app.UseServiceStack(new AppHost(new AppHostConfig()
            {
                BaseUri = new Uri(this.configuration["Bookmarks.Api:Uri"]),
                Auth0ClientId = this.configuration["Auth0:ClientId"],
                Auth0ClientSecret = this.configuration["Auth0:ClientSecret"].Replace('-', '+').Replace('_', '/'),
                Auth0Domain = this.configuration["Auth0:Domain"]
            }));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBookmarkManager, BookmarkManager>();
            services.AddSingleton<IDbConnectionFactory>(s => new OrmLiteConnectionFactory(this.configuration["Database:ConnectionString"], SqlServerDialect.Provider));
            services.AddSingleton<IDatabase, Database>();

            SqlMapper.AddTypeHandler(new UriTypeHandler());
        }
    }
}
