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

                if (!context.AddressTypes.Any())
                {
                    context.AddressTypes.AddRange(new AddressType { ExternalId = 1, Name = "Rua", State = EntityState.Added.ToString(), Active = true });
                }
                if (!context.CivilStatus.Any())
                {
                    context.CivilStatus.AddRange(new CivilStatus { ExternalId = 1, Name = "Casado", State = EntityState.Added.ToString(), Active = true});
                }
                if (!context.CivilStatus.Any())
                {
                    context.Countries.AddRange(new Country { ExternalId = 1, Name = "Brasil", State = EntityState.Added.ToString(), Active = true});
                }
                if (!context.Genders.Any())
                {
                    context.Genders.AddRange(new Gender { ExternalId = 1, Name = "Masculino", State = EntityState.Added.ToString(), Active = true});
                }
                if (!context.Nationalities.Any())
                {
                    context.Nationalities.AddRange(new Nationality { ExternalId = 1, Name = "Brasileiro", State = EntityState.Added.ToString(), Active = true});
                }                
                if (!context.PhoneTypes.Any())
                {
                    context.PhoneTypes.AddRange(new PhoneType { ExternalId = 1, Name = "Celular", State = EntityState.Added.ToString(), Active = true});
                }                                
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new Race { ExternalId = 1, Name = "Negro", State = EntityState.Added.ToString(), Active = true});
                }                                  
                if (!context.Schools.Any())
                {
                    context.Schools.AddRange(new School { ExternalId = 1, Name = "Colégio Padre Eustáquio", State = EntityState.Added.ToString(), Active = true});
                }                                                                

                context.SaveChanges();
            }
        }
    }
}