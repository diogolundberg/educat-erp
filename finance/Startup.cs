using System;
using AutoMapper;
using System.Net;
using System.Text;
using finance.Models;
using Newtonsoft.Json;
using SharpRaven.Core;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using finance.JsonFormatter;

namespace finance
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
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(Configuration["FINANCE_DATABASE_CONNECTION"]));
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
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        options.SerializerSettings.Converters.Add(new PolymorphicRepresentativeViewModelConverter());
                    });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "FINANCE", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
            });
            services.AddAutoMapper();
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1"); });
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

            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
