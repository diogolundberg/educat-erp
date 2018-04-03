using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.Validations;
using Onboarding.ViewModels;
using System;
using System.Collections;
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
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public EnrollmentsController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("{token}", Name = "ONBOARDING/ENROLLMENTS/GET")]
        public dynamic Get(string token)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("PersonalData")
                                            .Include("PersonalData.PersonalDataDisabilities")
                                            .Include("PersonalData.PersonalDataSpecialNeeds")
                                            .Include("PersonalData.PersonalDataDocuments")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("FinanceData")
                                            .Include("FinanceData.Representative")
                                            .Include("FinanceData.Guarantors")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document")
                                            .SingleOrDefault(x => x.ExternalId == token);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Link para matrícula inválido." } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "O prazo para esta matrícula foi encerrado." } });
            }

            PersonalDataViewModel personalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData);
            personalData.State = PersonalDataState(enrollment);

            FinanceDataViewModel financeData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData);
            financeData.State = FinanceDataState(enrollment);

            return new
            {
                data = new
                {
                    enrollment.Deadline,
                    enrollment.SentAt,
                    enrollment.AcademicApproval,
                    enrollment.FinanceApproval,
                    personalData,
                    financeData,
                },
                options = new
                {
                    _context.Genders,
                    _context.MaritalStatuses,
                    _context.States,
                    _context.Cities,
                    _context.Countries,
                    _context.AddressKinds,
                    _context.Races,
                    _context.HighSchoolKinds,
                    _context.Disabilities,
                    _context.SpecialNeeds,
                    _context.Plans,
                    _context.PaymentMethod,
                    PersonalDocuments = _context.Set<PersonalDocumentType>(),
                    GuarantorDocuments = _context.Set<GuarantorDocumentType>()
                }
            };
        }

        [HttpPost("{token}", Name = "ONBOARDING/ENROLLMENTS/SEND")]
        public dynamic Send(string token)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("PersonalData")
                                            .Include("PersonalData.PersonalDataDisabilities")
                                            .Include("PersonalData.PersonalDataSpecialNeeds")
                                            .Include("PersonalData.PersonalDataDocuments")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("FinanceData")
                                            .Include("FinanceData.Representative")
                                            .Include("FinanceData.Guarantors")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document")
                                            .Single(x => x.ExternalId == token);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Link para matrícula inválido" } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "O prazo para esta matrícula foi encerrado" } });
            }

            PersonalDataViewModel personalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData);
            personalData.State = PersonalDataState(enrollment);

            FinanceDataViewModel financeData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData);
            financeData.State = FinanceDataState(enrollment);

            var messages = new List<string>();

            if (personalData.State == "valid" && financeData.State == "valid")
            {
                enrollment.SentAt = DateTime.Now;
                _context.Set<Enrollment>().Update(enrollment);
                _context.SaveChanges();
                messages.Add("A matrícula foi enviada para aprovação");

                return new OkObjectResult(new { messages });
            }
            else
            {
                if (personalData.State != "valid")
                {
                    messages.Add("Os dados pessoais estão inválidos");
                }
                if (financeData.State != "valid")
                {
                    messages.Add("Os dados financeiros estão inválidos");
                }

                return new BadRequestObjectResult(new { messages });
            }
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
                    },
                    FinanceData = new FinanceData
                    {
                        Representative = new RepresentativePerson()
                    }
                };

                string externalId = enrollment.CreateExternalId();

                Enrollment existingEnrollment = _context.Enrollments.SingleOrDefault(x => x.ExternalId == externalId);

                if (existingEnrollment == null)
                {
                    _context.Enrollments.Add(enrollment);
                    _context.SaveChanges();
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

        private string PersonalDataState(Enrollment enrollment)
        {
            EnrollmentValidator validator = new EnrollmentValidator();
            FluentValidation.Results.ValidationResult results = validator.Validate(enrollment);

            if (!enrollment.PersonalData.UpdatedAt.HasValue)
            {
                return "empty";
            }

            if (results.IsValid)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }

        private string FinanceDataState(Enrollment enrollment)
        {
            EnrollmentValidator validator = new EnrollmentValidator();
            FluentValidation.Results.ValidationResult results = validator.Validate(enrollment);

            if (!enrollment.FinanceData.UpdatedAt.HasValue)
            {
                return "empty";
            }

            if (results.IsValid)
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
