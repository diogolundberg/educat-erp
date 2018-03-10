using System.Linq;
using Onboarding.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onboarding.Data.Entity;

namespace Onboarding
{
    public static class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetService<DatabaseContext>();       
                BaseRepository<AddressType> addressTypeRepository = new BaseRepository<AddressType>(context);
                BaseRepository<CivilStatus> civilStatusRepository = new BaseRepository<CivilStatus>(context);
                BaseRepository<Country> countryRepository = new BaseRepository<Country>(context);
                BaseRepository<Gender> genderRepository = new BaseRepository<Gender>(context);
                BaseRepository<Nationality> nationalityRepository = new BaseRepository<Nationality>(context);
                BaseRepository<PhoneType> phoneTypeRepository = new BaseRepository<PhoneType>(context);
                BaseRepository<Race> raceRepository = new BaseRepository<Race>(context);
                BaseRepository<School> schoolRepository = new BaseRepository<School>(context);
                BaseRepository<State> stateRepository = new BaseRepository<State>(context);
                BaseRepository<Disability> disabilityRepository = new BaseRepository<Disability>(context);

                context.Database.Migrate();
                context.Database.EnsureCreated();

                if (!context.AddressTypes.Any())
                {
                    addressTypeRepository.Add(new AddressType { Name = "Rua" });
                }
                if (!context.CivilStatus.Any())
                {
                    civilStatusRepository.Add(new CivilStatus { Name = "Casado" });
                }
                if (!context.CivilStatus.Any())
                {
                    countryRepository.Add(new Country { Name = "Brasil"});
                }
                if (!context.Genders.Any())
                {
                    genderRepository.Add(new Gender { Name = "Masculino" });
                }
                if (!context.Nationalities.Any())
                {
                    nationalityRepository.Add(new Nationality { Name = "Brasileiro" });
                }                
                if (!context.PhoneTypes.Any())
                {
                    phoneTypeRepository.Add(new PhoneType { Name = "Celular" });
                }                                
                if (!context.Races.Any())
                {
                    raceRepository.Add(new Race { Name = "Negro" });
                }                                  
                if (!context.Schools.Any())
                {
                    schoolRepository.Add(new School { Name = "Colégio Padre Eustáquio" });
                }
                if (!context.States.Any())                                                               
                {
                    stateRepository.Add(new State { Name = "MG" });
                } 
                if (!context.Disabilities.Any())                                                               
                {
                    disabilityRepository.Add(new Disability { Name = "Cegueira" });
                    disabilityRepository.Add(new Disability { Name = "Cegueira" });
                    disabilityRepository.Add(new Disability { Name = "Baixa Visão" });
                    disabilityRepository.Add(new Disability { Name = "Surdez" });
                    disabilityRepository.Add(new Disability { Name = "Surdocegueira" });
                    disabilityRepository.Add(new Disability { Name = "Deficiência Auditiva" });
                    disabilityRepository.Add(new Disability { Name = "Deficiência Física" });
                    disabilityRepository.Add(new Disability { Name = "Autismos" });
                    disabilityRepository.Add(new Disability { Name = "Transtorno Degenrativo de Infância" });
                    disabilityRepository.Add(new Disability { Name = "Altas Habilidades/Superdotação" });
                    disabilityRepository.Add(new Disability { Name = "Síndrome de Rett" });
                    disabilityRepository.Add(new Disability { Name = "Deficiência Intelectual" });
                    disabilityRepository.Add(new Disability { Name = "Sindrome de Aspergers" });
                    disabilityRepository.Add(new Disability { Name = "Deficiência Múltipla" });
                } 
                
                context.SaveChanges();
            }
        }
    }
}


