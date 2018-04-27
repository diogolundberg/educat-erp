using System;
using System.Linq;
using SSO.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using SharpRaven.Core.Configuration;
using SharpRaven.Core;
using Microsoft.AspNetCore.Http;
using SharpRaven.Core.Data;

namespace SSO
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

            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(Configuration["SSO_DATABASE_CONNECTION"]));

            services.AddScoped<IRavenClient, RavenClient>((s) =>
            {

                RavenClient rc = new RavenClient(Configuration["SENTRY_API"], s.GetRequiredService<IHttpContextAccessor>())
                {
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                };

                return rc;
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

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

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SSO", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DatabaseInitializer.Seed(app);

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SSO V1");
            });

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
                                await ravenClient.CaptureAsync(new SentryEvent(ex.Error));

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
                serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.EnsureCreated();
                serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            }

            app.UseMvc();
        }
    }
}
