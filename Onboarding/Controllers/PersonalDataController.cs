using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
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
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<PersonalData> _personalDataRepository;

        public PersonalDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _personalDataRepository = new BaseRepository<PersonalData>(_context);
        }

        [HttpPost("{token}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]PersonalDataViewModel obj)
        {

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            PersonalData personalData = _context.Set<PersonalData>()
                                                .Include(x => x.Enrollment)
                                                .Include(x => x.PersonalDataDisabilities)
                                                .Include(x => x.PersonalDataSpecialNeeds)
                                                .Include(x => x.PersonalDataDocuments)
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
                    .Any(c => c.PersonalDataId == personalDataDocument.PersonalDataId && c.DocumentId == personalDataDocument.DocumentId))
                {
                    _context.Set<PersonalDataDocument>().Remove(personalDataDocument);
                    _context.Set<Document>().Remove(_context.Set<Document>().Find(personalDataDocument.DocumentId));
                }
            }
            foreach (PersonalDataDocument personalDataDocument in personalDataMapped.PersonalDataDocuments)
            {
                PersonalDataDocument existingPersonalDataDocument = personalData.PersonalDataDocuments
                    .Where(c => c.PersonalDataId == personalDataDocument.PersonalDataId && c.DocumentId == personalDataDocument.DocumentId)
                    .SingleOrDefault();

                if (existingPersonalDataDocument != null)
                {
                    _context.Entry(existingPersonalDataDocument).CurrentValues.SetValues(personalDataDocument);
                }
                else
                {
                    personalData.PersonalDataDocuments.Add(personalDataDocument);
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
