using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonalDataController : Controller
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

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            PersonalData personalData = _context.Set<PersonalData>()
                                                .Include("Enrollment")
                                                .Include("PersonalDataDisabilities")
                                                .Include("PersonalDataSpecialNeeds")
                                                .Include("PersonalDataDocuments")
                                                .Include("PersonalDataDocuments.Document")
                                                .Single(x => x.Enrollment.ExternalId == token);

            if (personalData == null)
            {
                return NotFound();
            }

            if (personalData.Enrollment.SendDate.HasValue || !personalData.Enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            PersonalData personalDataMapped = _mapper.Map<PersonalData>(obj);
            _context.Entry(personalData).CurrentValues.SetValues(personalDataMapped);

            foreach (PersonalDataDocument personalDataDocument in personalData.PersonalDataDocuments.ToList())
            {
                if (!personalDataMapped
                    .PersonalDataDocuments
                    .Any(c => c.Document.Id == personalDataDocument.DocumentId || c.Document.DocumentTypeId == personalDataDocument.Document.DocumentTypeId))
                {
                    _context.Set<PersonalDataDocument>().Remove(personalDataDocument);
                    _context.Set<Document>().Remove(_context.Set<Document>().Find(personalDataDocument.DocumentId));
                }
            }
            foreach (PersonalDataDocument personalDataDocument in personalDataMapped.PersonalDataDocuments)
            {
                PersonalDataDocument existingPersonalDataDocument = personalData.PersonalDataDocuments
                    .Where(c => c.DocumentId == personalDataDocument.Document.Id || c.Document.DocumentTypeId == personalDataDocument.Document.DocumentTypeId)
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

            PersonalDataViewModel viewModel = _mapper.Map<PersonalDataViewModel>(personalData);
            viewModel.State = PersonalDataState(viewModel);

            var errors = ModelState.ToDictionary(
                modelState => modelState.Key,
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return new OkObjectResult(new
            {
                errors,
                data = viewModel
            });
        }

        private string PersonalDataState(PersonalDataViewModel personalData)
        {
            System.ComponentModel.DataAnnotations.ValidationContext context = new System.ComponentModel.DataAnnotations.ValidationContext(personalData);
            List<ValidationResult> result = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(personalData, context, result, true);

            if (!personalData.UpdatedAt.HasValue)
            {
                return "empty";
            }

            if (valid)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }
    }
}