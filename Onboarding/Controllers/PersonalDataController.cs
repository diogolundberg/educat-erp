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
                                                .AsNoTracking()
                                                .Single(x => x.Enrollment.ExternalId == token);

            if (personalData == null)
            {
                return NotFound();
            }

            if (personalData.Enrollment.SendDate.HasValue || !personalData.Enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            personalData = _mapper.Map<PersonalData>(obj);
            foreach (PersonalDataDocument document in personalData.PersonalDataDocuments)
            {
                if (document.Document.Id == 0)
                {
                    _context.Set<PersonalDataDocument>().Add(document);
                }
                else
                {
                    _context.Set<PersonalDataDocument>().Update(document);
                }
            }

            _context.Set<PersonalData>().Update(personalData);
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
