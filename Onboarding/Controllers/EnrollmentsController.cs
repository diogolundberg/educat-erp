using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

            PersonalDataViewModel personalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData);
            personalData.State = PersonalDataState(personalData);

            return new
            {
                data = new
                {
                    enrollment.Deadline,
                    enrollment.SendDate,
                    enrollment.AcademicApproval,
                    enrollment.FinanceApproval,
                    personalData,
                    FinanceData = enrollment.FinanceData != null ?
                                    _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData) :
                                    new FinanceDataViewModel { Representative = new RepresentativePersonViewModel() },
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

        [HttpPost("GenerateToken", Name = "ONBOARDING/ENROLLMENTS/GENERATETOKEN")]
        public IActionResult GenerateToken([FromBody]GenerateToken obj)
        {
            if (!ModelState.IsValid || obj.List.Count == 0)
            {
                return BadRequest();
            }

            List<string> responseObj = new List<string>();

            foreach (GenerateTokenEnrollment enrollmentParameterObj in obj.List)
            {
                Enrollment enrollment = new Enrollment
                {
                    Deadline = obj.End,
                    PersonalData = new PersonalData
                    {
                        RealName = enrollmentParameterObj.Name,
                        Email = enrollmentParameterObj.Email,
                        CPF = enrollmentParameterObj.CPF,
                    }
                };

                string externalId = enrollment.CreateExternalId();

                Enrollment existingEnrollment = _context.Enrollments.SingleOrDefault(x => x.ExternalId == externalId);

                if (existingEnrollment == null)
                {
                    _enrollmentRepository.Add(enrollment);
                }
                else
                {
                    enrollment = existingEnrollment;
                }

                SmtpClientHelper smtpClientHelper = new SmtpClientHelper(_configuration["SMTP_PORT"], _configuration["SMTP_HOST"], _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]);

                string body = string.Format("Clique <a href='{0}'>aqui</a> para se matricular", _configuration["ENROLLMENT_HOST"] + enrollment.ExternalId);
                string subject = _configuration["EMAIL_ENROLLMENTS_SUBJECT"];

                smtpClientHelper.Send(new MailAddress(_configuration["EMAIL_SENDER_ONBOARDING"]), new MailAddress(enrollmentParameterObj.Email), body, subject);

                responseObj.Add(string.Format("{0} - {1}", enrollmentParameterObj.Email, enrollment.ExternalId));
            }

            return new OkObjectResult(responseObj);
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
