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
                BaseRepository<Disability> disabilityRepository = new BaseRepository<Disability>(context);
                BaseRepository<DocumentType> documentTypeRepository = new BaseRepository<DocumentType>(context);
                BaseRepository<Gender> genderRepository = new BaseRepository<Gender>(context);
                BaseRepository<Nationality> nationalityRepository = new BaseRepository<Nationality>(context);
                BaseRepository<PhoneType> phoneTypeRepository = new BaseRepository<PhoneType>(context);
                BaseRepository<Race> raceRepository = new BaseRepository<Race>(context);
                BaseRepository<School> schoolRepository = new BaseRepository<School>(context);
                BaseRepository<State> stateRepository = new BaseRepository<State>(context);

                if (!context.AddressTypes.Any())
                {
                    addressTypeRepository.Add(new AddressType { Name = "Aeroporto" });
                    addressTypeRepository.Add(new AddressType { Name = "Alameda" });
                    addressTypeRepository.Add(new AddressType { Name = "Área" });
                    addressTypeRepository.Add(new AddressType { Name = "Avenida" });
                    addressTypeRepository.Add(new AddressType { Name = "Campo" });
                    addressTypeRepository.Add(new AddressType { Name = "Chácara" });
                    addressTypeRepository.Add(new AddressType { Name = "Colônia" });
                    addressTypeRepository.Add(new AddressType { Name = "Condomínio" });
                    addressTypeRepository.Add(new AddressType { Name = "Conjunto" });
                    addressTypeRepository.Add(new AddressType { Name = "Distrito" });
                    addressTypeRepository.Add(new AddressType { Name = "Esplanada" });
                    addressTypeRepository.Add(new AddressType { Name = "Estação" });
                    addressTypeRepository.Add(new AddressType { Name = "Estrada" });
                    addressTypeRepository.Add(new AddressType { Name = "Favela" });
                    addressTypeRepository.Add(new AddressType { Name = "Feira" });
                    addressTypeRepository.Add(new AddressType { Name = "Jardim" });
                    addressTypeRepository.Add(new AddressType { Name = "Ladeira" });
                    addressTypeRepository.Add(new AddressType { Name = "Lago" });
                    addressTypeRepository.Add(new AddressType { Name = "Lagoa" });
                    addressTypeRepository.Add(new AddressType { Name = "Largo" });
                    addressTypeRepository.Add(new AddressType { Name = "Loteamento" });
                    addressTypeRepository.Add(new AddressType { Name = "Morro" });
                    addressTypeRepository.Add(new AddressType { Name = "Núcleo" });
                    addressTypeRepository.Add(new AddressType { Name = "Loteamento" });
                    addressTypeRepository.Add(new AddressType { Name = "Parque" });
                    addressTypeRepository.Add(new AddressType { Name = "Passarela" });
                    addressTypeRepository.Add(new AddressType { Name = "Pátio" });
                    addressTypeRepository.Add(new AddressType { Name = "Praça" });
                    addressTypeRepository.Add(new AddressType { Name = "Quadra" });
                    addressTypeRepository.Add(new AddressType { Name = "Pátio" });
                    addressTypeRepository.Add(new AddressType { Name = "Recanto" });
                    addressTypeRepository.Add(new AddressType { Name = "Residencial" });                                                            
                    addressTypeRepository.Add(new AddressType { Name = "Rodovia" });                                                            
                    addressTypeRepository.Add(new AddressType { Name = "Rua" });                                                            
                    addressTypeRepository.Add(new AddressType { Name = "Setor" });
                    addressTypeRepository.Add(new AddressType { Name = "Sítio" });
                    addressTypeRepository.Add(new AddressType { Name = "Travessa" });
                    addressTypeRepository.Add(new AddressType { Name = "Trecho" });                    
                    addressTypeRepository.Add(new AddressType { Name = "Trevo" });                    
                    addressTypeRepository.Add(new AddressType { Name = "Vale" });
                    addressTypeRepository.Add(new AddressType { Name = "Vereda" });                    
                    addressTypeRepository.Add(new AddressType { Name = "Via" });                    
                    addressTypeRepository.Add(new AddressType { Name = "Viaduto" });                                        
                    addressTypeRepository.Add(new AddressType { Name = "Viela" });                                                            
                    addressTypeRepository.Add(new AddressType { Name = "Vila" });                    
                }
                
                if (!context.CivilStatus.Any())
                {
                    civilStatusRepository.Add(new CivilStatus { Name = "Solteiro(a)" });
                    civilStatusRepository.Add(new CivilStatus { Name = "Casado(a)" });
                    civilStatusRepository.Add(new CivilStatus { Name = "Divorciado(a)" });
                    civilStatusRepository.Add(new CivilStatus { Name = "Viúvo(a)" }); 
                    civilStatusRepository.Add(new CivilStatus { Name = "Separado(a)" });                                       
                }
                
                if (!context.Countries.Any())
                {
                    countryRepository.Add(new Country { Name = "Brasil"});
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

                if (!context.DocumentTypes.Any())
                {
                    documentTypeRepository.Add(new DocumentType { Name = "RG" });
                    documentTypeRepository.Add(new DocumentType { Name = "CNH" });
                    documentTypeRepository.Add(new DocumentType { Name = "Passaporte" });
                    documentTypeRepository.Add(new DocumentType { Name = "CTPS" });
                }

                if (!context.Genders.Any())
                {
                    genderRepository.Add(new Gender { Name = "Masculino" });
                    genderRepository.Add(new Gender { Name = "Feminino" });
                }

                if (!context.Nationalities.Any())
                {
                    nationalityRepository.Add(new Nationality { Name = "Brasileiro" });
                }                

                if (!context.PhoneTypes.Any())
                {
                    phoneTypeRepository.Add(new PhoneType { Name = "Celular" });
                    phoneTypeRepository.Add(new PhoneType { Name = "Residencial" });
                    phoneTypeRepository.Add(new PhoneType { Name = "Comercial" });
                }                                

                if (!context.Races.Any())
                {
                    raceRepository.Add(new Race { Name = "Brancos" });
                    raceRepository.Add(new Race { Name = "Negros" });
                    raceRepository.Add(new Race { Name = "Indígenas" });
                    raceRepository.Add(new Race { Name = "Pardos" });
                    raceRepository.Add(new Race { Name = "Mulatos" });
                    raceRepository.Add(new Race { Name = "Caboclos" });
                    raceRepository.Add(new Race { Name = "Cafuzos" });                    
                }                                  
                if (!context.Schools.Any())
                {
                    schoolRepository.Add(new School { Name = "Colégio Padre Eustáquio" });
                }
                if (!context.States.Any())                                                               
                {
                    stateRepository.Add(new State { Name = "AC" });
                    stateRepository.Add(new State { Name = "AL" });
                    stateRepository.Add(new State { Name = "AP" });
                    stateRepository.Add(new State { Name = "AM" });
                    stateRepository.Add(new State { Name = "BA" });
                    stateRepository.Add(new State { Name = "CE" });
                    stateRepository.Add(new State { Name = "DF" });
                    stateRepository.Add(new State { Name = "ES" });
                    stateRepository.Add(new State { Name = "GO" });
                    stateRepository.Add(new State { Name = "MA" });
                    stateRepository.Add(new State { Name = "MT" });
                    stateRepository.Add(new State { Name = "MS" });
                    stateRepository.Add(new State { Name = "MG" });
                    stateRepository.Add(new State { Name = "PA" });
                    stateRepository.Add(new State { Name = "PB" });
                    stateRepository.Add(new State { Name = "PR" });
                    stateRepository.Add(new State { Name = "PE" });
                    stateRepository.Add(new State { Name = "PI" });
                    stateRepository.Add(new State { Name = "RJ" });
                    stateRepository.Add(new State { Name = "RN" });
                    stateRepository.Add(new State { Name = "RS" });
                    stateRepository.Add(new State { Name = "RO" });
                    stateRepository.Add(new State { Name = "RR" });
                    stateRepository.Add(new State { Name = "SC" });
                    stateRepository.Add(new State { Name = "SP" });
                    stateRepository.Add(new State { Name = "SE" });
                    stateRepository.Add(new State { Name = "TO" });
                } 

                context.SaveChanges();
            }
        }
    }
}


