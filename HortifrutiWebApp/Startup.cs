using HortifrutiWebApp.Contracts;
using HortifrutiWebApp.Data;
using HortifrutiWebApp.Entities;
using HortifrutiWebApp.Models.Entities;
using HortifrutiWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HortifrutiWebApp
{
    public class Startup
    {
        #region Dependency Injection
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        #endregion

        #region Configure Services
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configure Cookie Policy
            services.Configure<CookiePolicyOptions>(options =>
            {
                // Habilita a necessidade de consentimento para uso de cookie
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #endregion

            #region Add Identity User
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //default = false
                options.Password.RequireNonAlphanumeric = false; //default = true
                options.Password.RequireUppercase = false; //default = true
                options.Password.RequireUppercase = false; //default = true
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //default = 3
                options.Lockout.MaxFailedAccessAttempts = 3; //default = 5
                options.SignIn.RequireConfirmedAccount = false; //default = true
                options.SignIn.RequireConfirmedEmail = true; //default = true
                options.SignIn.RequireConfirmedPhoneNumber = false; //default = true

            }).AddEntityFrameworkStores<WebAppDbContext>()
              .AddDefaultTokenProviders();
            #endregion

            #region Configure Application Cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Login";
                options.SlidingExpiration = true;
            });
            #endregion

            #region Add Authorization
            services.AddAuthorization(options =>
            {
                // Politica de acesso isAdmin
                options.AddPolicy("isAdmin", policy => policy.RequireRole("admin"));
            });
            #endregion

            #region Add Razor Pages
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Admin/Admin", "isAdmin");
                options.Conventions.AuthorizeFolder("/Admin/Products", "isAdmin");
            }).AddCookieTempDataProvider(options =>
            {
                options.Cookie.IsEssential = true;
            });
            #endregion

            #region Add MVC
            services.AddMvc();
            #endregion

            #region Add DB Context
            services.AddDbContext<WebAppDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("WebAppDbContext"), builder =>
                    builder.MigrationsAssembly("HortifrutiWebApp")));
            #endregion

            #region Configure Email
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddSingleton<IEmailSender, EmailSender>();
            //services.AddSingleton<IEmailSender, SendGridSender>();
            #endregion
        }
        #endregion

        #region Configure
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
            }

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);
        }
        #endregion
    }
}