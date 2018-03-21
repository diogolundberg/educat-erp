using AutoMapper;
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
        private readonly TokenHelper _tokenHelper;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<PersonalData> _personalDataRepository;

        public PersonalDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _configuration = configuration;
            _mapper = mapper;
            _tokenHelper = new TokenHelper();
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _personalDataRepository = new BaseRepository<PersonalData>(_context);
        }

        [HttpPatch("{token}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update(string token, [FromBody]PersonalDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            EnrollmentToken enrollmentToken = _tokenHelper.GetObject<EnrollmentToken>(token);

            if (!enrollmentToken.IsValid())
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetById(enrollmentToken.Id);
            _context.Entry(enrollment).Reference(x => x.PersonalData).Load();
            _context.Entry(enrollment.PersonalData).Collection(x => x.PersonalDataDisabilities).Load();

            if (enrollment == null)
            {
                return NotFound();
            }

            PersonalData newPersonalData = _mapper.Map<PersonalData>(obj);
            newPersonalData.ExternalId = enrollment.PersonalData.ExternalId;
            newPersonalData.EnrollmentId = enrollment.Id;

            _personalDataRepository.Update(enrollment.PersonalData, newPersonalData);

            return Ok();
        }
    }
}