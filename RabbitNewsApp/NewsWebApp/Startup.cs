using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitNews.Infrastructure.Jobs.NewsReceiverService;
using RabbitNews.Infrastructure.Database;
using Microsoft.Extensions.Options;
using MediatR;
using System.Reflection;
using RabbitNews.Application;
using RabbitNews.Domain.NewsCommands;
using RabbitNews.Domain.NewsCommands.Commands;
using RabbitNews.Application.NewsQueries.Queries;
using System;
using RabbitNews.Infrastructure.RabbitMqSettings;

namespace NewsWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // CRUD-API
            services.AddSingleton<INewsReadApi, NewsReadApi>();
            services.AddSingleton<INewsWriteApi, NewsWriteApi>();


            // MediatR - Queries/Commands 
            services.AddMediatR(typeof(GetAllNewsQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(NewsErstellenCommand).GetTypeInfo().Assembly);

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // MongoDb Setup
            services.Configure<NewsDatabaseSettings>(Configuration.GetSection(nameof(NewsDatabaseSettings)));
            services.AddSingleton<INewsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<NewsDatabaseSettings>>().Value);

            // RabbitMq Connection Settings
            services.Configure<RabbitMqConnectionSettings>(Configuration.GetSection(nameof(RabbitMqConnectionSettings)));
            services.AddSingleton<IRabbitMqConnectionSettings>(sp => sp.GetRequiredService<IOptions<RabbitMqConnectionSettings>>().Value);

            // NewsReceiverService RabbitMQ
            services.AddHostedService<NewsReceiverService>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
