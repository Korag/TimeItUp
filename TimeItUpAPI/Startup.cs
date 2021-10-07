using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TimeItUpAPI.Data;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;
using TimeItUpData.Library.Repositories;

namespace TimeItUpAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BasicIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<EFDbContext>();

            #region DI

            services.AddSingleton<IAlarmRepository, AlarmRepository>();
            services.AddSingleton<IIdentityAccountRepository, IdentityAccountRepository>();
            services.AddSingleton<ISplitRepository, SplitRepository>();
            services.AddSingleton<ITimerRepository, TimerRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IGeneralRepository, GeneralRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<BasicIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BasicIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EFDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
                    c.RoutePrefix = string.Empty;
                }
            );

            CheckIfDbHasBeenCreated(context);
        }

        public void CheckIfDbHasBeenCreated(EFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
