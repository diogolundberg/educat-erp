using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.Validations;
using Onboarding.Validations.FinanceData;
using Onboarding.Validations.PersonalData;
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
    public class EnrollmentsController : BaseController
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
                                            .Include("Onboarding")
                                            .Include("Pendencies")
                                            .Include("Pendencies.Section")
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
            personalData.Status = PersonalDataState(enrollment.PersonalData);

            FinanceDataViewModel financeData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData);
            financeData.Status = FinanceDataState(enrollment.FinanceData);

            EnrollmentMessagesValidator enrollmentValidator = new EnrollmentMessagesValidator(_context);
            FluentValidation.Results.ValidationResult enrollmentValidatorResult = enrollmentValidator.Validate(enrollment);
            List<string> messages = enrollmentValidatorResult.Errors.Select(x => x.ErrorMessage).Distinct().ToList();

            return new
            {
                messages,
                data = new
                {
                    OnboardingYear = enrollment.Onboarding.Year,
                    ReviewedAt = enrollment.ReviewedAt,
                    StartedAt = enrollment.StartedAt,
                    Deadline = enrollment.Onboarding.EndAt,
                    SentAt = enrollment.SentAt,
                    enrollment.Photo,
                    personalData,
                    financeData,
                    academicApproval = new
                    {
                        status = enrollment.AcademicApprovalStatus,
                        pendencies = enrollment.AcademicPendencies.Select(x => new { x.Description, x.SectionId, x.Section.Name })
                    },
                    financeApproval = new
                    {
                        status = enrollment.FinanceApprovalStatus,
                        pendencies = enrollment.FinancePendencies.Select(x => new { x.Description, x.SectionId, x.Section.Name }),
                    }
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
                    Plans = _context.Plans.Select(x => new { x.Id, x.Name, x.Guarantors, Value = x.Value.ToString("c"), x.Installments, x.DueDate }),
                    _context.PaymentMethod,
                    PersonalDocuments = _context.Set<PersonalDocumentType>(),
                    GuarantorDocuments = _context.Set<GuarantorDocumentType>(),
                    _context.Nationalities,
                    Relationships = _context.Relationships.Select(x => new { x.Id, x.Name, x.CheckStudentIsRepresentative })
                }
            };
        }

        [HttpPost("{token}", Name = "ONBOARDING/ENROLLMENTS/EDIT")]
        public dynamic Send(string token)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Onboarding")
                                            .Include("Pendencies")
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

            if (!string.IsNullOrEmpty(enrollment.SentAt))
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Estes dados já foram enviados para a análises e não pode ser editados no momento." } });
            }

            if (string.IsNullOrEmpty(enrollment.StartedAt))
            {
                enrollment.StartedAt = DateTime.Now.ToString("dd/MM/yyyy");

                _context.Set<Enrollment>().Update(enrollment);
                _context.SaveChanges();

                return Ok();
            }

            PersonalDataViewModel personalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData);
            personalData.Status = PersonalDataState(enrollment.PersonalData);

            FinanceDataViewModel financeData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData);
            financeData.Status = FinanceDataState(enrollment.FinanceData);

            EnrollmentMessagesValidator enrollmentValidator = new EnrollmentMessagesValidator(_context);
            FluentValidation.Results.ValidationResult enrollmentValidatorResult = enrollmentValidator.Validate(enrollment);
            List<string> messages = enrollmentValidatorResult.Errors.Select(x => x.ErrorMessage).Distinct().ToList();

            if (personalData.Status == "valid" && financeData.Status == "valid" && enrollmentValidatorResult.IsValid)
            {
                enrollment.SentAt = DateTime.Now.ToString("dd/MM/yyyy");
                enrollment.ReviewedAt = null;

                _context.Set<Enrollment>().Update(enrollment);
                _context.SaveChanges();

                string body = GetEmailBody("enrollment_sent.html");
                string subject = "Sua matricula foi enviada para análise";
                string messageBody = GetEmailBody("enrollment_sent.html")
                                     .Replace("{student_name}", enrollment.PersonalData.RealName)
                                     .Replace("{send_at}", enrollment.SentAt)
                                     .Replace("{send_at_hour}", DateTime.Now.ToString("HH:mm"));

                SendEmail(messageBody, subject, _configuration["EMAIL_SENDER_ONBOARDING"], enrollment.PersonalData.Email, _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]);

                return Ok();
            }
            else
            {
                if (personalData.Status != "valid")
                {
                    messages.Add("Os dados pessoais estão inválidos");
                }
                if (financeData.Status != "valid")
                {
                    messages.Add("Os dados financeiros estão inválidos");
                }

                return new BadRequestObjectResult(new { messages });
            }
        }

        private string PersonalDataState(PersonalData personalData)
        {
            PersonalDataValidator validator = new PersonalDataValidator(_context);
            FluentValidation.Results.ValidationResult results = validator.Validate(personalData);

            if (!personalData.UpdatedAt.HasValue)
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

        private string FinanceDataState(FinanceData financeData)
        {
            FinanceDataValidator validator = new FinanceDataValidator(_context);
            FluentValidation.Results.ValidationResult results = validator.Validate(financeData);
            FinanceDataMessagesValidator messagesValidator = new FinanceDataMessagesValidator(_context);
            FluentValidation.Results.ValidationResult resultsMessages = messagesValidator.Validate(financeData);

            if (!financeData.UpdatedAt.HasValue)
            {
                return "empty";
            }
            if (results.IsValid && resultsMessages.IsValid)
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
