using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
using Onboarding.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    public class EnrollmentsController : Controller
    {
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

        public EnrollmentsController(DatabaseContext databaseContext, IConfiguration configuration)
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
            _configuration = configuration;
        }

        [HttpGet("{token}")]
        public dynamic Get (string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            byte[] data = Convert.FromBase64String(token);
            string enrollmentTokenJson = Encoding.ASCII.GetString(data);
            EnrollmentToken enrollmentToken = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrollmentToken>(enrollmentTokenJson);

            if(DateTime.Now >= enrollmentToken.End)
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetById(enrollmentToken.Id);

            if(enrollment == null)
            {
                return NotFound();
            }

            return new { 
                data = enrollment,
                options = new 
                {
                    AddressTypes = _addressTypeRepository.List(),
                    CivilStatus = _civilStatusRepository.List(),
                    Countries = _countryRepository.List(),
                    Genders = _genderRepository.List(),
                    Nationalities = _nationalityRepository.List(),
                    PhoneTypes = _phoneTypeRepository.List(),
                    Races = _RaceRepository.List(),
                    Schools = _schoolRepository.List(),
                    States = _stateRepository.List(),
                    Disabilities = _disabilitiesRepository.List()
                }
            };
        }

        [HttpPatch("{id}", Name = "ONBOARDING/ENROLLMENTS/EDIT")]
        public IActionResult Update(Guid id, Enrollment obj)
        {
            if (obj == null || obj.Id != id)
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetById(obj.ExternalId);

            if (enrollment == null)
            {
                return NotFound();
            }

            if(enrollment.SendBy.HasValue)
            {
                return BadRequest();
            }

            Enrollment newEnrollment = (Enrollment)enrollment.Clone();

            _enrollmentRepository.Update(enrollment, newEnrollment);

            return Ok();
        }

       [HttpGet("GenerateToken", Name = "SSO/PASSWORD/NEW")]
        public IActionResult GenerateToken(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }            
            
            Enrollment enrollment = new Enrollment { Email = email };

            _enrollmentRepository.Add(enrollment);

            EnrollmentToken enrollmentToken = new EnrollmentToken { Id = enrollment.ExternalId, End = DateTime.Now.AddMonths(1) };
            string enrollmentTokenJson = Newtonsoft.Json.JsonConvert.SerializeObject(enrollmentToken);

            byte[] bytes = Encoding.ASCII.GetBytes(enrollmentTokenJson);
            string token = Convert.ToBase64String(bytes.ToArray());

            string emailText = String.Format("?token=" + token);
            string apiKey = _configuration["SENDGRID_APIKEY"];
            string plainTextContent = Regex.Replace(emailText, "<[^>]*>", "");

            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress("suporte@cmmg.com.br", "Suporte");
            EmailAddress to = new EmailAddress(email);  
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, "subject", plainTextContent, emailText);
            SendGrid.Response response = client.SendEmailAsync(msg).Result;

            return new OkObjectResult(new { token = token });
        }
    }
}