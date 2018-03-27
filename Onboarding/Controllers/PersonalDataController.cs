using System.Linq;
using AutoMapper;
using Marvin.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<PersonalData> _personalDataRepository;

        public PersonalDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _configuration = configuration;
            _mapper = mapper;
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _personalDataRepository = new BaseRepository<PersonalData>(_context);
        }

        [HttpPatch("{token}", Name = "ONBOARDING/PERSONALDATA/PATCHEDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]JsonPatchDocument<PersonalDataPatchViewModel> personalDataPatch)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            Enrollment enrollmentDatabase = _enrollmentRepository.GetByExternalId(token);

            if (enrollmentDatabase.SendDate.HasValue || !enrollmentDatabase.IsDeadlineValid())
            {
                return BadRequest();
            }

            _context.Entry(enrollmentDatabase).Reference(x => x.PersonalData).Load();

            PersonalDataPatchViewModel personalDataViewModel = _mapper.Map<PersonalDataPatchViewModel>(enrollmentDatabase.PersonalData);
            personalDataPatch.ApplyTo(personalDataViewModel);

            _mapper.Map(personalDataViewModel, enrollmentDatabase.PersonalData);

            _personalDataRepository.Update(enrollmentDatabase.PersonalData);

            var errors = ModelState.ToDictionary(
                modelState => modelState.Key,
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return new OkObjectResult(new
            {
                errors,
                data = _mapper.Map<PersonalDataPatchViewModel>(enrollmentDatabase.PersonalData)
            });
        }

        [HttpPost("{token}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]PersonalDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetByExternalId(token);
            _context.Entry(enrollment).Reference(x => x.PersonalData).Load();

            if (enrollment == null)
            {
                return NotFound();
            }

            if (enrollment.SendDate.HasValue || !enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            PersonalData personalData = enrollment.PersonalData;
            personalData = _mapper.Map<PersonalData>(obj);

            _personalDataRepository.Update(personalData);

            var errors = ModelState.ToDictionary(
                modelState => modelState.Key,
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return new OkObjectResult(new
            {
                errors,
                data = _mapper.Map<PersonalDataViewModel>(personalData)
            });
        }

    }
}
