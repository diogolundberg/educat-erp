using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onboarding.Data.Entity;
using Onboarding.Models;
using System.Collections.Generic;

namespace Onboarding
{
    public static class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                context.Database.Migrate();

                BaseRepository<AddressKind> addressKindRepository = new BaseRepository<AddressKind>(context);
                BaseRepository<MaritalStatus> maritalStatusRepository = new BaseRepository<MaritalStatus>(context);
                BaseRepository<Country> countryRepository = new BaseRepository<Country>(context);
                BaseRepository<Disability> disabilityRepository = new BaseRepository<Disability>(context);
                BaseRepository<Gender> genderRepository = new BaseRepository<Gender>(context);
                BaseRepository<Race> raceRepository = new BaseRepository<Race>(context);
                BaseRepository<HighSchoolKind> highSchoolRepository = new BaseRepository<HighSchoolKind>(context);
                BaseRepository<State> stateRepository = new BaseRepository<State>(context);
                BaseRepository<PersonalDocumentType> personalDocumentRepository = new BaseRepository<PersonalDocumentType>(context);
                BaseRepository<GuarantorDocumentType> guarantorDocumentRepository = new BaseRepository<GuarantorDocumentType>(context);

                if (!addressKindRepository.Any())
                {
                    addressKindRepository.Add(new AddressKind { Name = "Aeroporto" });
                    addressKindRepository.Add(new AddressKind { Name = "Alameda" });
                    addressKindRepository.Add(new AddressKind { Name = "Área" });
                    addressKindRepository.Add(new AddressKind { Name = "Avenida" });
                    addressKindRepository.Add(new AddressKind { Name = "Campo" });
                    addressKindRepository.Add(new AddressKind { Name = "Chácara" });
                    addressKindRepository.Add(new AddressKind { Name = "Colônia" });
                    addressKindRepository.Add(new AddressKind { Name = "Condomínio" });
                    addressKindRepository.Add(new AddressKind { Name = "Conjunto" });
                    addressKindRepository.Add(new AddressKind { Name = "Distrito" });
                    addressKindRepository.Add(new AddressKind { Name = "Esplanada" });
                    addressKindRepository.Add(new AddressKind { Name = "Estação" });
                    addressKindRepository.Add(new AddressKind { Name = "Estrada" });
                    addressKindRepository.Add(new AddressKind { Name = "Favela" });
                    addressKindRepository.Add(new AddressKind { Name = "Feira" });
                    addressKindRepository.Add(new AddressKind { Name = "Jardim" });
                    addressKindRepository.Add(new AddressKind { Name = "Ladeira" });
                    addressKindRepository.Add(new AddressKind { Name = "Lago" });
                    addressKindRepository.Add(new AddressKind { Name = "Lagoa" });
                    addressKindRepository.Add(new AddressKind { Name = "Largo" });
                    addressKindRepository.Add(new AddressKind { Name = "Loteamento" });
                    addressKindRepository.Add(new AddressKind { Name = "Morro" });
                    addressKindRepository.Add(new AddressKind { Name = "Núcleo" });
                    addressKindRepository.Add(new AddressKind { Name = "Loteamento" });
                    addressKindRepository.Add(new AddressKind { Name = "Parque" });
                    addressKindRepository.Add(new AddressKind { Name = "Passarela" });
                    addressKindRepository.Add(new AddressKind { Name = "Pátio" });
                    addressKindRepository.Add(new AddressKind { Name = "Praça" });
                    addressKindRepository.Add(new AddressKind { Name = "Quadra" });
                    addressKindRepository.Add(new AddressKind { Name = "Pátio" });
                    addressKindRepository.Add(new AddressKind { Name = "Recanto" });
                    addressKindRepository.Add(new AddressKind { Name = "Residencial" });
                    addressKindRepository.Add(new AddressKind { Name = "Rodovia" });
                    addressKindRepository.Add(new AddressKind { Name = "Rua" });
                    addressKindRepository.Add(new AddressKind { Name = "Setor" });
                    addressKindRepository.Add(new AddressKind { Name = "Sítio" });
                    addressKindRepository.Add(new AddressKind { Name = "Travessa" });
                    addressKindRepository.Add(new AddressKind { Name = "Trecho" });
                    addressKindRepository.Add(new AddressKind { Name = "Trevo" });
                    addressKindRepository.Add(new AddressKind { Name = "Vale" });
                    addressKindRepository.Add(new AddressKind { Name = "Vereda" });
                    addressKindRepository.Add(new AddressKind { Name = "Via" });
                    addressKindRepository.Add(new AddressKind { Name = "Viaduto" });
                    addressKindRepository.Add(new AddressKind { Name = "Viela" });
                    addressKindRepository.Add(new AddressKind { Name = "Vila" });
                }

                if (!maritalStatusRepository.Any())
                {
                    maritalStatusRepository.Add(new MaritalStatus { Name = "Solteiro(a)" });
                    maritalStatusRepository.Add(new MaritalStatus { Name = "Casado(a)" });
                    maritalStatusRepository.Add(new MaritalStatus { Name = "Divorciado(a)" });
                    maritalStatusRepository.Add(new MaritalStatus { Name = "Viúvo(a)" });
                    maritalStatusRepository.Add(new MaritalStatus { Name = "Separado(a)" });
                }

                if (!countryRepository.Any())
                {
                    countryRepository.Add(new Country { Name = "Brasil" });
                }

                if (!disabilityRepository.Any())
                {
                    disabilityRepository.Add(new Disability
                    {
                        Name = "Cegueira",
                        SpecialNeeds = new List<SpecialNeed>
                        {
                            new SpecialNeed { Name = "Prova Braile" },
                            new SpecialNeed { Name = "Auxilio para leitura/escrita – ledor" },
                            new SpecialNeed { Name = "Prova Ampliada" },
                            new SpecialNeed { Name = "Prova Ampliada (Fonte Tamanho 16)" },
                            new SpecialNeed { Name = "Prova Ampliada (Fonte Tamanho 20)" },
                            new SpecialNeed { Name = "Prova Ampliada (Fonte Tamanho 24)" },
                        }
                    });
                    disabilityRepository.Add(new Disability
                    {
                        Name = "Deficiência Auditiva",
                        SpecialNeeds = new List<SpecialNeed>
                        {
                            new SpecialNeed { Name = "Usa aparelho auditivo" },
                            new SpecialNeed { Name = "Não usa aparelho auditivo, apesar da deficiência auditiva" },
                            new SpecialNeed { Name = "Intérprete de Libras" },
                            new SpecialNeed { Name = "Leitura Labial" },
                        }
                    });
                    disabilityRepository.Add(new Disability
                    {
                        Name = "Deficiência física",
                        SpecialNeeds = new List<SpecialNeed>
                        {
                            new SpecialNeed { Name = "Auxilio para transcrição - deficiência de membros superiores" },
                            new SpecialNeed { Name = "Mesa adaptada para cadeira de rodas" },
                        }
                    });
                    disabilityRepository.Add(new Disability { Name = "Deficiência Intelectual" });
                    disabilityRepository.Add(new Disability { Name = "Deficiência Múltipla" });
                    disabilityRepository.Add(new Disability { Name = "Visão Subnormal ou baixa visão" });
                    disabilityRepository.Add(new Disability { Name = "Surdez" });
                    disabilityRepository.Add(new Disability { Name = "Surdocegueira" });
                    disabilityRepository.Add(new Disability { Name = "Autismo infantil" });
                    disabilityRepository.Add(new Disability { Name = "Síndrome de Asperger" });
                    disabilityRepository.Add(new Disability { Name = "Síndrome de Relt" });
                    disabilityRepository.Add(new Disability { Name = "Transtorno desintegrativo da infância" });
                    disabilityRepository.Add(new Disability { Name = "Altas habilidades/superdotação" });
                }

                if (!genderRepository.Any())
                {
                    genderRepository.Add(new Gender { Name = "Masculino" });
                    genderRepository.Add(new Gender { Name = "Feminino" });
                }

                if (!raceRepository.Any())
                {
                    raceRepository.Add(new Race { Name = "Brancos" });
                    raceRepository.Add(new Race { Name = "Negros" });
                    raceRepository.Add(new Race { Name = "Indígenas" });
                    raceRepository.Add(new Race { Name = "Pardos" });
                    raceRepository.Add(new Race { Name = "Mulatos" });
                    raceRepository.Add(new Race { Name = "Caboclos" });
                    raceRepository.Add(new Race { Name = "Cafuzos" });
                }

                if (!highSchoolRepository.Any())
                {
                    highSchoolRepository.Add(new HighSchoolKind { Name = "TESTE" });
                }

                if (!personalDocumentRepository.Any())
                {
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "Histórico Escolar do Ensino Médio" });
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "Certidão de Nascimento ou Casamento" });
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "Carteira de Identidade" });
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "Título de Eleitor e Comprovante de Votação" });
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "CPF" });
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "Cartão de Vacinação (constanto 3 doses de vacina contra Hepatite B e vacina Dupla-adulto" });
                    personalDocumentRepository.Add(new PersonalDocumentType { Name = "Documento Militar" });
                }

                if (!guarantorDocumentRepository.Any())
                {
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "Histórico Escolar do Ensino Médio" });
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "Certidão de Nascimento ou Casamento" });
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "Carteira de Identidade" });
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "Título de Eleitor e Comprovante de Votação" });
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "CPF" });
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "Cartão de Vacinação (constanto 3 doses de vacina contra Hepatite B e vacina Dupla-adulto" });
                    guarantorDocumentRepository.Add(new GuarantorDocumentType { Name = "Documento Militar" });
                }
                if (!stateRepository.Any())
                {
                    stateRepository.Add(new State
                    {
                        Name = "AC",
                        Cities = new List<City>
                        {
                            new City { Name = "Acrelândia" }
                        }
                    });
                }
                context.SaveChanges();
            }
        }
    }
}


