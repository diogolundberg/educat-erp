using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Models;
using Onboarding.Statuses;
using Onboarding.Validations;
using Onboarding.Validations.PersonalData;
using Onboarding.ViewModels;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonalDataController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public PersonalDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpPost("{token}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]PersonalDataViewModel obj)
        {
            PersonalData personalData = _context.Set<PersonalData>()
                                                .Include("Enrollment.Onboarding")
                                                .Include("Enrollment")
                                                .Include("Enrollment.Pendencies")
                                                .Include("Enrollment.FinanceData")
                                                .Include("Enrollment.FinanceData.Guarantors")
                                                .Include("Enrollment.FinanceData.Representative")
                                                .Include("PersonalDataDocuments")
                                                .Include("PersonalDataSpecialNeeds")
                                                .Include("PersonalDataDisabilities")
                                                .Include("PersonalDataDocuments.Document")
                                                .SingleOrDefault(x => x.Enrollment.ExternalId == token);

            if (personalData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Link para matrícula inválido." } });
            }

            if (!personalData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "O prazo para esta matrícula foi encerrado." } });
            }

            if (!personalData.Editable)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Estes dados já foram enviados para a análises e não pode ser editados no momento." } });
            }

            PersonalData personalDataMapped = _mapper.Map<PersonalData>(obj);
            _context.Entry(personalData).CurrentValues.SetValues(personalDataMapped);

            foreach (PersonalDataDocument personalDataDocument in personalData.PersonalDataDocuments.ToList())
            {
                if (!personalDataMapped
                    .PersonalDataDocuments
                    .Any(c => c.Document.Id == personalDataDocument.DocumentId))
                {
                    _context.Set<PersonalDataDocument>().Remove(personalDataDocument);
                    _context.Set<Document>().Remove(_context.Set<Document>().Find(personalDataDocument.DocumentId));
                }
            }
            foreach (PersonalDataDocument personalDataDocument in personalDataMapped.PersonalDataDocuments)
            {
                PersonalDataDocument existingPersonalDataDocument = personalData.PersonalDataDocuments
                    .Where(c => c.DocumentId == personalDataDocument.Document.Id)
                    .SingleOrDefault();

                if (existingPersonalDataDocument != null)
                {
                    personalDataDocument.Document.Id = existingPersonalDataDocument.Document.Id;
                    _context.Entry(existingPersonalDataDocument.Document).CurrentValues.SetValues(personalDataDocument.Document);
                }
                else
                {
                    personalDataDocument.PersonalDataId = personalData.Id;
                    personalDataDocument.Document.Id = 0;
                    _context.Set<PersonalDataDocument>().Add(personalDataDocument);
                }
            }

            foreach (PersonalDataDisability personalDataDisibility in personalData.PersonalDataDisabilities.ToList())
            {
                if (!personalDataMapped
                    .PersonalDataDisabilities
                    .Any(c => c.DisabilityId == personalDataDisibility.DisabilityId))
                {
                    _context.Set<PersonalDataDisability>().Remove(personalDataDisibility);
                }
            }
            foreach (PersonalDataDisability personalDataDisability in personalDataMapped.PersonalDataDisabilities)
            {
                PersonalDataDisability existingPersonalDataDisability = personalData.PersonalDataDisabilities
                    .Where(c => c.DisabilityId == personalDataDisability.DisabilityId)
                    .SingleOrDefault();

                if (existingPersonalDataDisability == null)
                {
                    personalData.PersonalDataDisabilities.Add(personalDataDisability);
                }
            }

            foreach (PersonalDataSpecialNeed personalDataSpecialNeed in personalData.PersonalDataSpecialNeeds.ToList())
            {
                if (!personalDataMapped
                    .PersonalDataSpecialNeeds
                    .Any(c => c.SpecialNeedId == personalDataSpecialNeed.SpecialNeedId))
                {
                    _context.Set<PersonalDataSpecialNeed>().Remove(personalDataSpecialNeed);
                }
            }
            foreach (PersonalDataSpecialNeed personalDataSpecialNeed in personalDataMapped.PersonalDataSpecialNeeds)
            {
                PersonalDataSpecialNeed existingPersonalDataSpecialNeed = personalData.PersonalDataSpecialNeeds
                    .Where(c => c.SpecialNeedId == personalDataSpecialNeed.SpecialNeedId)
                    .SingleOrDefault();

                if (existingPersonalDataSpecialNeed == null)
                {
                    personalData.PersonalDataSpecialNeeds.Add(personalDataSpecialNeed);
                }
            }

            _context.Entry(personalData).Property(x => x.CPF).IsModified = false;
            _context.Entry(personalData).Property(x => x.RealName).IsModified = false;
            _context.Entry(personalData).Property(x => x.Email).IsModified = false;

            _context.SaveChanges();
            _context.Entry(personalData).Reload();
            _context.Entry(personalData).Reference(x => x.Nationality).Load();
            _context.Entry(personalData).Reference(x => x.Gender).Load();
            _context.Entry(personalData).Reference(x => x.HighSchoolGraduationCountry).Load();

            PersonalDataViewModel viewModel = _mapper.Map<PersonalDataViewModel>(personalData);
            viewModel.Status = (new PersonalDataStatus(new PersonalDataValidator(_context), personalData)).GetStatus();

            PersonalDataValidator validator = new PersonalDataValidator(_context);
            Hashtable errors = FormatErrors(validator.Validate(personalData));

            return new OkObjectResult(new
            {
                errors,
                data = viewModel
            });
        }
    }
}
