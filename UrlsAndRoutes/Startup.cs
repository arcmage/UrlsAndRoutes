﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("weekday", typeof(WeekDayConstraint))); // To add Inline custom constraint
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "MyRoute",
                    template: "{controller=Home}/{action=Index}/{id:weekday?}");
                //                    template: "{controller}/{action}/{id?}",
                //                    defaults: new { controller = "Home", action = "Index" },
                //                    constraints: new { id = new WeekDayConstraint() });

                routes.MapRoute(name: "MyRoute0",
                    template: "{controller:regex(^H.*)=Home}/"
                              + "{action:regex(^Index$|^About$)=Index}/{id?}");

                routes.MapRoute(name: "MyRoute2",
                    template: "{controller=Home}/{action=Index}/{id:range(10,20)?}");

                routes.MapRoute(name: "MyRoute3",
                    template: "{controller=Home}/{action=Index}"
                              + "/{id:alpha:minlength(6)?}");
            });
        }
    }
}
