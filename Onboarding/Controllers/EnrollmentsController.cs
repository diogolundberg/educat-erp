using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using onboarding.Models;
using onboarding.Statuses;
using onboarding.Validations;
using onboarding.Validations.FinanceData;
using onboarding.Validations.PersonalData;
using onboarding.ViewModels;
using onboarding.ViewModels.Enrollments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
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
                                            .Include("Appointments")
                                            .SingleOrDefault(x => x.ExternalId == token);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            Record record = _mapper.Map<Record>(enrollment);

            record.PersonalData.Status = (new PersonalDataStatus(new PersonalDataValidator(_context), enrollment.PersonalData)).GetStatus();
            record.FinanceData.Status = (new FinanceDataStatus(new FinanceDataValidator(_context), enrollment.FinanceData, new FinanceDataMessagesValidator(_context))).GetStatus();

            EnrollmentValidator enrollmentValidator = new EnrollmentValidator(_context);
            FluentValidation.Results.ValidationResult enrollmentValidatorResult = enrollmentValidator.Validate(enrollment);
            List<string> messages = enrollmentValidatorResult.Errors.Select(x => x.ErrorMessage).Distinct().ToList();

            if (record.Finished)
            {
                return new
                {
                    data = record
                };
            }
            else
            {
                return new
                {
                    messages,
                    data = record,
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
                        Plans = _context.Plans.Select(x => new { x.Id, x.Name, x.Guarantors, x.Value, x.Installments, x.InstallmentValue, x.DueDate }),
                        _context.PaymentMethod,
                        PersonalDocuments = _context.Set<PersonalDocumentType>(),
                        GuarantorDocuments = _context.Set<GuarantorDocumentType>(),
                        _context.Nationalities,
                        Relationships = _context.Relationships.Select(x => new { x.Id, x.Name, x.CheckStudentIsRepresentative, x.CheckSpouse })
                    }
                };
            }
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
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            if (enrollment.SentAt.HasValue)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentInReview } });
            }

            if (!enrollment.StartedAt.HasValue)
            {
                enrollment.StartedAt = DateTime.Now;

                _context.Set<Enrollment>().Update(enrollment);
                _context.SaveChanges();

                return Ok();
            }

            PersonalDataViewModel personalData = _mapper.Map<PersonalDataViewModel>(enrollment.PersonalData);
            personalData.Status = (new PersonalDataStatus(new PersonalDataValidator(_context), enrollment.PersonalData)).GetStatus();

            FinanceDataViewModel financeData = _mapper.Map<FinanceDataViewModel>(enrollment.FinanceData);
            financeData.Status = (new FinanceDataStatus(new FinanceDataValidator(_context), enrollment.FinanceData, new FinanceDataMessagesValidator(_context))).GetStatus();

            EnrollmentValidator enrollmentValidator = new EnrollmentValidator(_context);
            FluentValidation.Results.ValidationResult enrollmentValidatorResult = enrollmentValidator.Validate(enrollment);
            List<string> messages = enrollmentValidatorResult.Errors.Select(x => x.ErrorMessage).Distinct().ToList();

            if (personalData.Status == "valid" && financeData.Status == "valid" && enrollmentValidatorResult.IsValid)
            {
                enrollment.SentAt = DateTime.Now;

                _context.Set<Enrollment>().Update(enrollment);
                _context.SaveChanges();

                string body = GetEmailBody("enrollment_sent.html");
                string subject = "Sua matricula foi enviada para análise";
                string messageBody = GetEmailBody("enrollment_sent.html")
                                     .Replace("{student_name}", enrollment.PersonalData.AssumedName)
                                     .Replace("{send_at}", enrollment.SentAt.Value.ToString("dd/MM/yyyy"))
                                     .Replace("{send_at_hour}", enrollment.SentAt.Value.ToString("HH:mm"));

                BackgroundJob.Enqueue(() => (new EmailHelper()).SendEmail(messageBody, subject, _configuration["EMAIL_SENDER"], enrollment.PersonalData.Email, _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]));

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
    }
}
