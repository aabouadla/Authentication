using IdentityExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("Memo");
            });

            // Add Identity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                // config the password 
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>() // to let the Identity service know to which Db that should communicate
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });

            // Grandmas.Cookie
            // Specifying a Cookie Handler to produce an implementation of an Authentication Handler
            // it will be injected in our service and will be used in app.UseAuthorization 
            // to check if the User Authenticated or not
            //services.AddAuthentication("CookieAuth")
            //    .AddCookie("CookieAuth", config =>
            //    {
            //        config.Cookie.Name = "Grandmas.Cookie";
            //        config.LoginPath = "/Home/Authenticate";
            //    });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // who are you
            app.UseAuthentication();

            // are you Authoreized (allowed)
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
