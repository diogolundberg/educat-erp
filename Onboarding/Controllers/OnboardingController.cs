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
            FormValidator formValidator = new FormValidator();
            ValidationResult validationResult = formValidator.Validate(obj);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new { Errors = FormatErrors(validationResult) });
            }

            Models.Onboarding existingOnboarding = _context.Onboardings.SingleOrDefault(x => x.Semester == obj.Semester && x.Year == obj.Year);

            if (existingOnboarding != null)
            {
                return new BadRequestObjectResult(new { Messages = new List<string> { "Já existe um período de matrícula cadastrado para este semestre e ano." } });
            }

            Models.Onboarding onboarding = _mapper.Map<Models.Onboarding>(obj);

            foreach (Enrollment enrollment in onboarding.Enrollments)
            {
                enrollment.ExternalId = onboarding.Year + onboarding.Semester + Regex.Replace(enrollment.PersonalData.CPF, @"\D", string.Empty);
                enrollment.FinanceData = new FinanceData
                {
                    Representative = new RepresentativePerson()
                };
            }

            _context.Onboardings.Add(onboarding);
            _context.SaveChanges();

            return new OkObjectResult(new
            {
                data = obj
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
