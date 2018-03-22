using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.ViewModel;
using System.Collections.Generic;
using System.Net.Mail;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EnrollmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<AddressKind> _addressKindRepository;
        private readonly BaseRepository<MaritalStatus> _maritalStatusRepository;
        private readonly BaseRepository<Country> _countryRepository;
        private readonly BaseRepository<Gender> _genderRepository;
        private readonly BaseRepository<Race> _RaceRepository;
        private readonly BaseRepository<HighSchoolKind> _highSchoolKindRepository;
        private readonly BaseRepository<State> _stateRepository;
        private readonly BaseRepository<Disability> _disabilitiesRepository;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<City> _cityRepository;
        private readonly BaseRepository<SpecialNeed> _specialNeedsRepository;
        private readonly BaseRepository<PersonalData> _personalDataRepository;
        private readonly BaseRepository<PersonalDocument> _personalDocumentsRepository;
        private readonly BaseRepository<GuarantorDocument> _guarantorDocumentsRepository;
        private readonly TokenHelper _tokenHelper;

        public EnrollmentsController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _addressKindRepository = new BaseRepository<AddressKind>(_context);
            _maritalStatusRepository = new BaseRepository<MaritalStatus>(_context);
            _countryRepository = new BaseRepository<Country>(_context);
            _genderRepository = new BaseRepository<Gender>(_context);
            _RaceRepository = new BaseRepository<Race>(_context);
            _highSchoolKindRepository = new BaseRepository<HighSchoolKind>(_context);
            _stateRepository = new BaseRepository<State>(_context);
            _disabilitiesRepository = new BaseRepository<Disability>(_context);
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _cityRepository = new BaseRepository<City>(_context);
            _specialNeedsRepository = new BaseRepository<SpecialNeed>(_context);
            _personalDataRepository = new BaseRepository<PersonalData>(_context);
            _personalDocumentsRepository = new BaseRepository<PersonalDocument>(_context);
            _guarantorDocumentsRepository = new BaseRepository<GuarantorDocument>(_context);

            _configuration = configuration;
            _mapper = mapper;
            _tokenHelper = new TokenHelper();
        }

        [HttpGet("{token}", Name = "ONBOARDING/ENROLLMENTS/GET")]
        public dynamic Get(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetByExternalId(token);

            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Entry(enrollment).Reference(x => x.PersonalData).Load();
            _context.Entry(enrollment.PersonalData).Collection(x => x.PersonalDataDisabilities).Load();
            _context.Entry(enrollment.PersonalData).Collection(x => x.PersonalDataSpecialNeeds).Load();
            _context.Entry(enrollment.PersonalData).Collection(x => x.PersonalDataDocuments).Load();

            foreach (PersonalDataDocument personalDataDocument in enrollment.PersonalData.PersonalDataDocuments)
            {
                _context.Entry(personalDataDocument).Reference(x => x.Document).Load();
            }

            if (!enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            return new
            {
                data = new
                {
                    Deadline = enrollment.Deadline,
                    SendDate = enrollment.SendDate,
                    AcademicApproval = enrollment.AcademicApproval,
                    FinanceApproval = enrollment.FinanceApproval,
                    PersonalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData),
                    FinanceData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData),
                },
                options = new
                {
                    Genders = _genderRepository.List(),
                    MaritalStatuses = _maritalStatusRepository.List(),
                    States = _stateRepository.List(),
                    Cities = _cityRepository.List(),
                    Countries = _countryRepository.List(),
                    AddressKinds = _addressKindRepository.List(),
                    Races = _RaceRepository.List(),
                    HighSchoolKinds = _highSchoolKindRepository.List(),
                    Disabilities = _disabilitiesRepository.List(),
                    SpecialNeeds = _specialNeedsRepository.List(),
                    PersonalDocuments = _personalDocumentsRepository.List(),
                    GuarantorDocuments = _guarantorDocumentsRepository.List()
                }
            };
        }

        [HttpPatch("{token}", Name = "ONBOARDING/ENROLLMENTS/EDIT")]
        public IActionResult Update(string token, [FromBody]EnrollmentViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetByExternalId(token);

            if (enrollment == null)
            {
                return NotFound();
            }

            if (enrollment.SendDate.HasValue || !enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("GenerateToken", Name = "ONBOARDING/ENROLLMENTS/GENERATETOKEN")]
        public IActionResult GenerateToken([FromBody]GenerateToken obj)
        {
            if (obj.List.Count == 0)
            {
                return BadRequest();
            }

            List<string> responseObj = new List<string>();

            foreach (GenerateTokenEnrollment enrollmentParameterObj in obj.List)
            {
                Enrollment enrollment = new Enrollment { 
                    Deadline = obj.End,
                    PersonalData = new PersonalData {
                        RealName = enrollmentParameterObj.Name,
                        Email = enrollmentParameterObj.Email,
                        CPF = enrollmentParameterObj.CPF,
                    }
                };

                _enrollmentRepository.Add(enrollment);

                SmtpClientHelper smtpClientHelper = new SmtpClientHelper(_configuration["SMTP_PORT"], _configuration["SMTP_HOST"], _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]);

                string body = string.Format("Clique <a href='{0}'>aqui</a> para se matricular", _configuration["ENROLLMENT_HOST"] + enrollment.ExternalId);
                string subject = _configuration["EMAIL_ENROLLMENTS_SUBJECT"];

                smtpClientHelper.Send(new MailAddress(_configuration["EMAIL_SENDER_ONBOARDING"]), new MailAddress(enrollmentParameterObj.Email), body, subject);

                responseObj.Add(string.Format("{0} - {1}", enrollmentParameterObj.Email, enrollment.ExternalId));
            }

            return new OkObjectResult(responseObj);
        }
    }
}
