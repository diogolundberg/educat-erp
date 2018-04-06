using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Models;
using Onboarding.Validations.Onboarding;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Globalization;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    public class OnboardingController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public OnboardingController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpPost("", Name = "ONBOARDING/CREATE")]
        public dynamic Post([FromBody]ViewModels.Onboarding.Form obj)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            FormValidator formValidator = new FormValidator();
            ValidationResult validationResult = formValidator.Validate(obj);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new { Errors = FormatErrors(validationResult) });
            }

            foreach (var item in obj.Enrollments)
            {
                Enrollment enrollment = new Enrollment
                {
                    Deadline = DateTime.ParseExact(obj.End, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PersonalData = new PersonalData
                    {
                        RealName = item.Name,
                        Email = item.Email,
                        CPF = item.Cpf,
                    },
                    FinanceData = new FinanceData
                    {
                        Representative = new RepresentativePerson()
                    }
                };

                string externalId = obj.Year + obj.Semester + Regex.Replace(item.Cpf, @"\D", string.Empty);

                Enrollment existingEnrollment = _context.Enrollments.Include("PersonalData").SingleOrDefault(x => x.ExternalId == externalId);

                if (existingEnrollment == null)
                {
                    _context.Enrollments.Add(enrollment);
                    _context.SaveChanges();

                    SendEmail(enrollment.ExternalId, enrollment.PersonalData.Email);
                }
                else
                {
                    enrollment = existingEnrollment;
                }

                enrollments.Add(enrollment);
            }

            return new OkObjectResult(new
            {
                data = enrollments.Select(x => new
                {
                    token = x.ExternalId,
                    name = x.PersonalData.RealName,
                    email = x.PersonalData.Email,
                    cpf = x.PersonalData.CPF
                })
            });
        }

        private void SendEmail(string externalId, string email)
        {
            SmtpClientHelper smtpClientHelper = new SmtpClientHelper(_configuration["SMTP_PORT"], _configuration["SMTP_HOST"], _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]);

            string body = string.Format("Clique <a href='{0}'>aqui</a> para se matricular", _configuration["ENROLLMENT_HOST"] + externalId);
            string subject = _configuration["EMAIL_ENROLLMENTS_SUBJECT"];

            smtpClientHelper.Send(new MailAddress(_configuration["EMAIL_SENDER_ONBOARDING"]), new MailAddress(email), body, subject);
        }
    }
}
