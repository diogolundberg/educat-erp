using System.Linq;
using Onboarding.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Onboarding
{
    public static class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();       
                
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
        }
    }
}