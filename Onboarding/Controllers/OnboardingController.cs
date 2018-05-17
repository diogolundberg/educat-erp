using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.Validations.Onboarding;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Hangfire;
using System;

namespace onboarding.Controllers
{
    public class OnboardingController : BaseController<Onboarding>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;

        public OnboardingController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet("", Name = "ONBOARDING/LIST")]
        public dynamic GetList()
        {
            List<Models.Onboarding> onboardings = _context.Onboardings.Include("Enrollments").ToList();
            List<ViewModels.Onboarding.Records> records = _mapper.Map<List<ViewModels.Onboarding.Records>>(onboardings);
            return new { records };
        }

        [HttpGet("{id}", Name = "ONBOARDING")]
        public dynamic Get(int id)
        {
            Onboarding onboarding = _context.Onboardings.Include("Enrollments.PersonalData").SingleOrDefault(x => x.Id == id);

            if (onboarding == null)
            {
                return new BadRequestObjectResult(new { Messages = new List<string> { Resources.Messages.OnboardingNotExisting } });
            }

            return _mapper.Map<ViewModels.Onboarding.Record>(onboarding);
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
                return new BadRequestObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingExisting } });
            }

            Models.Onboarding onboardingModel = _mapper.Map<Models.Onboarding>(obj);

            IEnumerable<IGrouping<string, Enrollment>> group = onboardingModel.Enrollments.GroupBy(x => x.PersonalData.CPF);

            if (group.Where(x => x.ToList().Count() > 1).Count() > 0)
            {
                return new BadRequestObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingDuplicateCpf } });
            }

            foreach (Enrollment enrollment in onboardingModel.Enrollments)
            {
                enrollment.ExternalId = onboardingModel.Year + onboardingModel.Semester + Regex.Replace(enrollment.PersonalData.CPF, @"\D", string.Empty);
                enrollment.StartedAt = DateTime.Now;
                enrollment.FinanceData = new FinanceData
                {
                    Representative = new RepresentativePerson()
                };
            }

            _context.Onboardings.Add(onboardingModel);
            _context.SaveChanges();

            foreach (Enrollment enrollment in onboardingModel.Enrollments)
            {
                string link = string.Format("http://cmmg-ui.netlify.com/enroll/{0}", enrollment.ExternalId);
                string messageBody = GetEmailBody("enrollment_invite.html").Replace("{link}", link);
                string subject = "Link da matrícula";

                BackgroundJob.Enqueue(() => (new EmailHelper()).SendEmail(messageBody, subject, _configuration["EMAIL_SENDER"], enrollment.PersonalData.Email, _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]));
            }

            return new OkObjectResult(new
            {
                data = _mapper.Map<ViewModels.Onboarding.Form>(onboardingModel)
            });
        }

        [HttpPut("{id}", Name = "ONBOARDING/EDIT")]
        public dynamic Put(int id, [FromBody]ViewModels.Onboarding.Form obj)
        {
            FormValidator formValidator = new FormValidator();
            ValidationResult validationResult = formValidator.Validate(obj);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new { Errors = FormatErrors(validationResult) });
            }

            Models.Onboarding existingOnboarding = _context.Onboardings
                                                           .Include("Enrollments")
                                                           .Include("Enrollments.PersonalData")
                                                           .Include("Enrollments.FinanceData")
                                                           .Include("Enrollments.FinanceData.Representative")
                                                           .SingleOrDefault(x => x.Id == id);

            if (existingOnboarding == null)
            {
                return new NotFoundObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingNotExisting } });
            }

            if (existingOnboarding.Enrollments.Any(x => x.StartedAt.HasValue))
            {
                return new NotFoundObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingEditBlock } });
            }

            Models.Onboarding onboardingModel = _mapper.Map<Models.Onboarding>(obj);

            IEnumerable<IGrouping<string, Enrollment>> group = onboardingModel.Enrollments.GroupBy(x => x.PersonalData.CPF);

            if (group.Where(x => x.ToList().Count() > 1).Count() > 0)
            {
                return new BadRequestObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingDuplicateCpf } });
            }

            foreach (Enrollment enrollment in existingOnboarding.Enrollments.ToList())
            {
                if (!onboardingModel.Enrollments.Any(c => c.Id == enrollment.Id))
                {
                    _context.Set<Representative>().Remove(enrollment.FinanceData.Representative);
                    _context.Set<FinanceData>().Remove(enrollment.FinanceData);
                    _context.Set<PersonalData>().Remove(enrollment.PersonalData);
                    _context.Set<Enrollment>().Remove(enrollment);
                }
            }
            foreach (Enrollment enrollment in onboardingModel.Enrollments)
            {
                Enrollment searchedEnrolment = existingOnboarding.Enrollments.SingleOrDefault(x => x.Id == enrollment.Id);

                if (searchedEnrolment != null)
                {
                    enrollment.PersonalData.Id = searchedEnrolment.PersonalData.Id;
                    enrollment.PersonalData.EnrollmentId = searchedEnrolment.Id;

                    _context.Entry(searchedEnrolment.PersonalData).CurrentValues.SetValues(enrollment.PersonalData);
                }
                else
                {
                    enrollment.ExternalId = onboardingModel.Year + onboardingModel.Semester + Regex.Replace(enrollment.PersonalData.CPF, @"\D", string.Empty);
                    enrollment.FinanceData = new FinanceData
                    {
                        Representative = new RepresentativePerson()
                    };

                    string link = string.Format("http://cmmg-ui.netlify.com/enroll/{0}", enrollment.ExternalId);
                    string messageBody = GetEmailBody("enrollment_invite.html").Replace("{link}", link);
                    string subject = "Link da matrícula";

                    BackgroundJob.Enqueue(() => (new EmailHelper()).SendEmail(messageBody, subject, _configuration["EMAIL_SENDER"], enrollment.PersonalData.Email, _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]));
                }
            }

            onboardingModel.Id = existingOnboarding.Id;
            _context.Entry(existingOnboarding).CurrentValues.SetValues(onboardingModel);
            _context.SaveChanges();
            _context.Entry(existingOnboarding).Reload();

            return new OkObjectResult(new
            {
                data = _mapper.Map<ViewModels.Onboarding.Form>(existingOnboarding)
            });
        }

        [HttpDelete("{id}", Name = "ONBOARDING/DELETE")]
        public dynamic Delete(int id)
        {
            Models.Onboarding existingOnboarding = _context.Onboardings
                                                           .Include("Enrollments")
                                                           .Include("Enrollments.PersonalData")
                                                           .Include("Enrollments.FinanceData")
                                                           .Include("Enrollments.FinanceData.Representative")
                                                           .SingleOrDefault(x => x.Id == id);

            if (existingOnboarding == null)
            {
                return new NotFoundObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingNotExisting } });
            }

            if (existingOnboarding.Enrollments.Any(x => x.StartedAt.HasValue))
            {
                return new NotFoundObjectResult(new { Messages = new List<string> { onboarding.Resources.Messages.OnboardingDeleteBlock } });
            }

            foreach (Enrollment enrollment in existingOnboarding.Enrollments)
            {
                _context.Representatives.Remove(enrollment.FinanceData.Representative);
                _context.FinanceDatas.Remove(enrollment.FinanceData);
                _context.PersonalDatas.Remove(enrollment.PersonalData);
                _context.Enrollments.Remove(enrollment);
            }

            _context.Onboardings.Remove(existingOnboarding);
            _context.SaveChanges();

            return Ok();
        }
    }
}
