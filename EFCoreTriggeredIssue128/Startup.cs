using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTriggeredIssue128
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("EFCoreTriggeredIssue128");

                options.UseTriggers(options =>
                {
                    options.AddTrigger<Triggers.Trigger1>();
                    options.AddTrigger<Triggers.Trigger2>();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    using var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();

                    var entity = new FooEntity();
                    dbContext.Foos.Add(entity);
                    dbContext.SaveChanges();

                    await context.Response.WriteAsync($"Trigger1: {entity.Trigger1DateTime}, Trigger2: {entity.Trigger2DateTime}");
                });
            });
        }
    }
}
