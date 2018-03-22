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

            Enrollment enrollment = _enrollmentRepository.GetById(enrollmentToken.ExternalId);

            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Entry(enrollment).Reference(x => x.PersonalData).Load();

            if (!enrollmentToken.IsValid(enrollment.PersonalData))
            {
                return BadRequest();
            }

            PersonalData oldPersonalData = enrollment.PersonalData;

            Enrollment newEnrollment = (Enrollment)enrollment.Clone();

            _enrollmentRepository.Update(enrollment, newEnrollment);

            _context.Entry(newEnrollment).Reference(x => x.PersonalData).Load();

            PersonalData newPersonalData = _mapper.Map<PersonalData>(obj);

            newPersonalData.ExternalId = newEnrollment.PersonalData.ExternalId;
            newPersonalData.EnrollmentId = newEnrollment.Id;
            newPersonalData.RealName = oldPersonalData.RealName;
            newPersonalData.CPF = oldPersonalData.CPF;
            newPersonalData.Email = oldPersonalData.Email;

            _personalDataRepository.Update(oldPersonalData, newPersonalData);

            return Ok();
        }
    }
}
