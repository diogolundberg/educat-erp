using System.Linq;
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
        public IActionResult Update(string token, [FromBody]PersonalDataPatchViewModel obj)
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


            return new OkObjectResult(new
            {
                ModelState,
                data = new
                {
                    Deadline = enrollment.Deadline,
                    SendDate = enrollment.SendDate,
                    AcademicApproval = enrollment.AcademicApproval,
                    FinanceApproval = enrollment.FinanceApproval,
                    PersonalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData),
                    FinanceData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData),
                }
            });
        }

    }
}
