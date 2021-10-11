using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TimeItUpAPI.Data;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;
using TimeItUpData.Library.Repositories;
using TimeItUpServices.Library.EmailService;
using TimeItUpServices.Library.EmailService.Profile;

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
            #region DbContext

            services.AddDbContext<BasicIdentityDbContext>(options =>
                                                          options.UseSqlServer(
                                                          Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<EFDbContext>();

            #endregion

            #region DI

            services.AddTransient<IAlarmRepository, AlarmRepository>();
            services.AddTransient<IIdentityAccountRepository, IdentityAccountRepository>();
            services.AddTransient<ISplitRepository, SplitRepository>();
            services.AddTransient<ITimerRepository, TimerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGeneralRepository, GeneralRepository>();

            services.AddTransient<IEmailServiceProvider, EmailServiceProvider>();
            services.AddSingleton<IEmailProviderConfigurationProfile>
                (Configuration.GetSection("EmailProviderConfiguration").Get<EmailProviderConfigurationProfile>());


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<BasicIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BasicIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            #region JWT Configuration

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Secrets:JwtKey"))),
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });

                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };

                c.AddSecurityDefinition("Jwt Authorization", securityDefinition);

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Jwt Authorization",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };

                c.AddSecurityRequirement(securityRequirements);
            });

            #endregion

            #region CORS

            services.AddCors(options =>
            {
                options.AddPolicy(name: Configuration.GetValue<string>("CORS:Name"),
                                  builder =>
                                  {
                                      builder.WithOrigins(Configuration.GetSection("CORS").GetSection("AllowedOrigins").Get<string[]>());
                                  });
            });

            #endregion
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

            //app.UseCors(Configuration.GetValue<string>("CORS:Name"));

            //CheckIfDbHasBeenCreated(context);
        }

        public void CheckIfDbHasBeenCreated(EFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
