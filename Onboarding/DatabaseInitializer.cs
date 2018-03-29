using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onboarding.Data.Entity;
using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

                if (!context.AddressKinds.Any())
                {
                    context.AddressKinds.Add(new AddressKind { Name = "Aeroporto" });
                    context.AddressKinds.Add(new AddressKind { Name = "Alameda" });
                    context.AddressKinds.Add(new AddressKind { Name = "Área" });
                    context.AddressKinds.Add(new AddressKind { Name = "Avenida" });
                    context.AddressKinds.Add(new AddressKind { Name = "Campo" });
                }

                if (!context.MaritalStatuses.Any())
                {
                    context.MaritalStatuses.Add(new MaritalStatus { Name = "Solteiro(a)" });
                    context.MaritalStatuses.Add(new MaritalStatus { Name = "Casado(a)" });
                    context.MaritalStatuses.Add(new MaritalStatus { Name = "Divorciado(a)" });
                    context.MaritalStatuses.Add(new MaritalStatus { Name = "Viúvo(a)" });
                    context.MaritalStatuses.Add(new MaritalStatus { Name = "Separado(a)" });
                }

                if (!context.Countries.Any())
                {
                    context.Countries.Add(new Country { Name = "Brasil" });
                }

                if (!context.Disabilities.Any())
                {
                    context.Disabilities.Add(new Disability
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
                    context.Disabilities.Add(new Disability
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
                    context.Disabilities.Add(new Disability
                    {
                        Name = "Deficiência física",
                        SpecialNeeds = new List<SpecialNeed>
                        {
                            new SpecialNeed { Name = "Auxilio para transcrição - deficiência de membros superiores" },
                            new SpecialNeed { Name = "Mesa adaptada para cadeira de rodas" },
                        }
                    });
                    context.Disabilities.Add(new Disability { Name = "Deficiência Intelectual" });
                    context.Disabilities.Add(new Disability { Name = "Deficiência Múltipla" });
                    context.Disabilities.Add(new Disability { Name = "Visão Subnormal ou baixa visão" });
                    context.Disabilities.Add(new Disability { Name = "Surdez" });
                    context.Disabilities.Add(new Disability { Name = "Surdocegueira" });
                    context.Disabilities.Add(new Disability { Name = "Autismo infantil" });
                    context.Disabilities.Add(new Disability { Name = "Síndrome de Asperger" });
                    context.Disabilities.Add(new Disability { Name = "Síndrome de Relt" });
                    context.Disabilities.Add(new Disability { Name = "Transtorno desintegrativo da infância" });
                    context.Disabilities.Add(new Disability { Name = "Altas habilidades/superdotação" });
                }

                if (!context.Genders.Any())
                {
                    context.Genders.Add(new Gender { Name = "Masculino" });
                    context.Genders.Add(new Gender { Name = "Feminino" });
                }

                if (!context.Races.Any())
                {
                    context.Races.Add(new Race { Name = "Brancos" });
                    context.Races.Add(new Race { Name = "Negros" });
                    context.Races.Add(new Race { Name = "Indígenas" });
                    context.Races.Add(new Race { Name = "Pardos" });
                    context.Races.Add(new Race { Name = "Mulatos" });
                    context.Races.Add(new Race { Name = "Caboclos" });
                    context.Races.Add(new Race { Name = "Cafuzos" });
                }

                if (!context.HighSchoolKinds.Any())
                {
                    context.HighSchoolKinds.Add(new HighSchoolKind { Name = "TESTE" });
                }

                if (!context.Set<PersonalDocumentType>().Any())
                {
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "Histórico Escolar do Ensino Médio" });
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "Certidão de Nascimento ou Casamento" });
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "Carteira de Identidade" });
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "Título de Eleitor e Comprovante de Votação" });
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "CPF" });
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "Cartão de Vacinação (constanto 3 doses de vacina contra Hepatite B e vacina Dupla-adulto" });
                    context.Set<PersonalDocumentType>().Add(new PersonalDocumentType { Name = "Documento Militar" });
                }

                if (!context.Set<GuarantorDocumentType>().Any())
                {
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "Histórico Escolar do Ensino Médio" });
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "Certidão de Nascimento ou Casamento" });
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "Carteira de Identidade" });
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "Título de Eleitor e Comprovante de Votação" });
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "CPF" });
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "Cartão de Vacinação (constanto 3 doses de vacina contra Hepatite B e vacina Dupla-adulto" });
                    context.Set<GuarantorDocumentType>().Add(new GuarantorDocumentType { Name = "Documento Militar" });
                }

                if (!context.States.Any())
                {
                    context.States.Add(new State
                    {
                        Name = "AC",
                        Cities = new List<City>
                        {
                            new City { Name = "Acrelândia" }
                        }
                    });
                }

                if (!context.Plans.Any())
                {
                    context.Plans.Add(new Plan { Name = "Anual", Value = decimal.Parse("82000.00"), Installments = 1, DueDate = DateTime.Now });
                    context.Plans.Add(new Plan { Name = "Semestral", Value = decimal.Parse("86000.00"), Installments = 2, DueDate = DateTime.Now });
                    context.Plans.Add(new Plan { Name = "Mensal", Value = decimal.Parse("99000.00"), Installments = 12, DueDate = DateTime.Now });
                }

                if (!context.PaymentMethod.Any())
                {
                    context.PaymentMethod.Add(new PaymentMethod { Name = "BOLETO" });
                }

                context.SaveChanges();
            }
        }
    }
}


