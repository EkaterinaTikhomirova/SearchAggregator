using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SearchAggregator.Repositories;
using SearchAggregator.Services;
using System;

namespace SearchAggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connection = _configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SearchAggregatorContext>(options =>
            {
                options.UseSqlServer(connection)
                .UseLoggerFactory(LoggerFactory.Create(buider => buider.AddConsole()));
            });
            services.AddScoped(typeof(IResourceRepository), typeof(ResourceRepository));
            services.AddScoped(typeof(IKeywordRepository), typeof(KeywordRepository));

            services.AddTransient<SearchService, CustomSearchServices>();
            services.AddTransient<IKeywordService, KeywordService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
