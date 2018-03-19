using System;
using AutoMapper;
using System.Linq;
using System.Text;
using Onboarding.Models;
using Onboarding.ViewModel;
using Onboarding.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    public class EnrollmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<AddressType> _addressTypeRepository;
        private readonly BaseRepository<CivilStatus> _civilStatusRepository;
        private readonly BaseRepository<Country> _countryRepository;
        private readonly BaseRepository<Gender> _genderRepository;
        private readonly BaseRepository<Nationality> _nationalityRepository;
        private readonly BaseRepository<PhoneType> _phoneTypeRepository;
        private readonly BaseRepository<Race> _RaceRepository;
        private readonly BaseRepository<School> _schoolRepository;
        private readonly BaseRepository<State> _stateRepository;
        private readonly BaseRepository<Disability> _disabilitiesRepository;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<DocumentType> _documentTypeRepository;
        private readonly TokenHelper _tokenHelper;

        public EnrollmentsController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _addressTypeRepository = new BaseRepository<AddressType>(_context);
            _civilStatusRepository = new BaseRepository<CivilStatus>(_context);
            _countryRepository = new BaseRepository<Country>(_context);
            _genderRepository = new BaseRepository<Gender>(_context);
            _nationalityRepository = new BaseRepository<Nationality>(_context);
            _phoneTypeRepository = new BaseRepository<PhoneType>(_context);
            _RaceRepository = new BaseRepository<Race>(_context);
            _schoolRepository = new BaseRepository<School>(_context);
            _stateRepository = new BaseRepository<State>(_context);
            _disabilitiesRepository = new BaseRepository<Disability>(_context);
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _documentTypeRepository = new BaseRepository<DocumentType>(_context);
            _configuration = configuration;
            _mapper = mapper;
            _tokenHelper = new TokenHelper();
        }

        [HttpGet("{token}")]
        public dynamic Get (string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            EnrollmentToken enrollmentToken = _tokenHelper.GetObject<EnrollmentToken>(token);

            if(!enrollmentToken.IsValid())
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetById(enrollmentToken.Id);

            if(enrollment == null)
            {
                return NotFound();
            }

            _context.Entry(enrollment).Collection(x => x.EnrollmentDisabilities).Load();
            _context.Entry(enrollment).Reference(x => x.Responsible).Load();
            _context.Entry(enrollment).Reference(x => x.Guarantor).Load();

            return new { 
                data = new {
                    Deadline = enrollmentToken.End,
                    PersonalData = enrollment
                },
                options = new 
                {   
                    AddressTypes = _addressTypeRepository.List(),
                    CivilStatus = _civilStatusRepository.List(),
                    Countries = _countryRepository.List(),
                    Disabilities = _disabilitiesRepository.List(),
                    DocumentTypes = _documentTypeRepository.List(),
                    Genders = _genderRepository.List(),
                    Nationalities = _nationalityRepository.List(),
                    PhoneTypes = _phoneTypeRepository.List(),
                    Races = _RaceRepository.List(),
                    Schools = _schoolRepository.List(),
                    States = _stateRepository.List()
                }
            };
        }

        [HttpPatch("{token}", Name = "ONBOARDING/ENROLLMENTS/EDIT")]
        public IActionResult Update(string token, [FromBody]EnrollmentViewModel obj)
        {
            if(string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            EnrollmentToken enrollmentToken = _tokenHelper.GetObject<EnrollmentToken>(token);

            if(!enrollmentToken.IsValid())
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetById(enrollmentToken.Id);

            if(enrollment == null)
            {
                return NotFound();
            }

            if(enrollment.SendBy.HasValue)
            {
                return BadRequest();
            }   

            Enrollment newEnrollment = _mapper.Map<Enrollment>(obj);
            newEnrollment.ExternalId = enrollment.ExternalId;

            _enrollmentRepository.Update(enrollment, newEnrollment);

            return Ok();
        }

       [HttpPost("GenerateToken", Name = "ONBOARDING/ENROLLMENTS/GENERATETOKEN")]
        public IActionResult GenerateToken([FromBody]EnrollmentParameter obj)
        {
            if(obj.Emails.Count == 0)
            {
                return BadRequest();
            }            
            
            List<string> responseObj = new List<string>();

            foreach (string email in obj.Emails)
            {
                Enrollment enrollment = new Enrollment { Email = email };

                _enrollmentRepository.Add(enrollment);

                EnrollmentToken enrollmentToken = new EnrollmentToken { Id = enrollment.ExternalId, End = obj.End, Start = obj.Start };
                string token = _tokenHelper.Generate<EnrollmentToken>(enrollmentToken);

                SmtpClientHelper smtpClientHelper = new SmtpClientHelper(_configuration["SMTP_PORT"],
                                                                        _configuration["SMTP_HOST"],
                                                                        _configuration["SMTP_USERNAME"],
                                                                        _configuration["SMTP_PASSWORD"]);

                string body = string.Format("Clique <a href='{0}'>aqui</a> para se matricular", _configuration["ENROLLMENT_HOST"] + token );
                string subject = _configuration["EMAIL_ENROLLMENTS_SUBJECT"];

                smtpClientHelper.Send(new MailAddress(_configuration["EMAIL_SENDER_ONBOARDING"]),
                                    new MailAddress(email),
                                    body,
                                    subject);

                responseObj.Add(string.Format("{0} - {1}", email, token));
            }

            return new OkObjectResult(responseObj);
        }
    }
}