using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using onboarding.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SharpRaven.Core;
using onboarding.JsonFormatter;
using System.Globalization;
using Hangfire;
using onboarding.Filters;
using onboarding.Services;
using onboarding.Resolvers;

namespace onboarding
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(Configuration["ONBOARDING_DATABASE_CONNECTION"]));
            services.AddScoped<IRavenClient, RavenClient>((s) =>
            {
                RavenClient rc = new RavenClient(Configuration["SENTRY_API"], s.GetRequiredService<IHttpContextAccessor>())
                {
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                };
                return rc;
            });
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["SSO_HOST"],
                    ValidAudience = Configuration["SSO_HOST"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SECURITY_KEY"]))
                };
            });
            services.Configure<MvcOptions>(options => { options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy")); });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new Info { Title = "ONBOARDING", Version = "v2" });
                c.CustomSchemaIds(x => x.FullName);
            });
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ONBOARDING_DATABASE_CONNECTION"]));
            services.AddMvc().AddJsonOptions(options =>
                            {
                                options.SerializerSettings.Converters.Add(new PolymorphicRepresentativeViewModelConverter());
                                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                            });

            services.AddScoped(typeof(BaseService<>), typeof(BaseService<>));
            services.AddScoped<EnrollmentService, EnrollmentService>();
            services.AddScoped<PersonalDataService, PersonalDataService>();
            services.AddScoped<CardResolver, CardResolver>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddAutoMapper(config => config.ConstructServicesUsing(serviceProvider.GetService));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2"); });
            app.UseExceptionHandler(
                builder =>
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        IRavenClient ravenClient = serviceScope.ServiceProvider.GetService<IRavenClient>();

                        builder.Run(async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";

                            IExceptionHandlerFeature ex = context.Features.Get<IExceptionHandlerFeature>();

                            if (ex != null)
                            {
                                var err = JsonConvert.SerializeObject(new Error()
                                {
                                    Stacktrace = ex.Error.StackTrace,
                                    Message = ex.Error.Message
                                });

                                await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                            }
                        });
                    }
                }
            );

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            }

            app.UseStaticFiles();
            app.UseMvc();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/dashboard", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorization() }
            });

            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
