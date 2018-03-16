using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharpRaven.Core;
using SharpRaven.Core.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace Upload
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

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

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

            app.UseMvc();
        }
    }
}
