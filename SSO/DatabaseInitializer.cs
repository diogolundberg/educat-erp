using System.Linq;
using SSO.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSO.Data.Entity;

namespace SSO
{
    public static class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                BaseRepository<User> userRepository = new BaseRepository<User>(context);
                BaseRepository<Feature> featureRepository = new BaseRepository<Feature>(context);

                context.Database.Migrate();
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    userRepository.Add(new User { Email = "lucasviniciusbatistacosta@gmail.com", Password = "123456" });
                    userRepository.Add(new User { Email = "amandarflavia@gmail.com", Password = "123456" });
                }

                if (!context.Features.Any())
                {
                    featureRepository.Add(new Feature { Grant = "SSO/USERS/GET" });
                    featureRepository.Add(new Feature { Grant = "SSO/USERS/NEW" });
                    featureRepository.Add(new Feature { Grant = "SSO/USERS/LIST" });
                    featureRepository.Add(new Feature { Grant = "SSO/USERS/DELETE" });
                    featureRepository.Add(new Feature { Grant = "SSO/USERS/EDIT" });
                }
            }
        }
    }
}
