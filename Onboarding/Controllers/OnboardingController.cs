using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Models;
using Onboarding.Validations.Onboarding;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Hangfire;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    public class OnboardingController : BaseController
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

            IEnumerable<IGrouping<string, Enrollment>> group = onboarding.Enrollments.GroupBy(x => x.PersonalData.CPF);

            if (group.Where(x => x.ToList().Count() > 1).Count() > 0)
            {
                return new BadRequestObjectResult(new { Messages = new List<string> { "Existem cpfs duplicados neste período de matrícula." } });
            }

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

            foreach (Enrollment enrollment in onboarding.Enrollments)
            {
                string link = string.Format("http://cmmg-ui.netlify.com/enroll/{0}", enrollment.ExternalId);
                string messageBody = GetEmailBody("enrollment_invite.html").Replace("{link}", link);
                string subject = "Link da matrícula";

                BackgroundJob.Enqueue(() => (new EmailHelper()).SendEmail(messageBody, subject, _configuration["EMAIL_SENDER"], enrollment.PersonalData.Email, _configuration["SMTP_USERNAME"], _configuration["SMTP_PASSWORD"]));
            }

            return new OkObjectResult(new
            {
                data = _mapper.Map<ViewModels.Onboarding.Form>(onboarding)
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
                return new NotFoundObjectResult(new { Messages = new List<string> { "Esse período de matrícula não existe." } });
            }

            if (existingOnboarding.Enrollments.Any(x => x.StartedAt.HasValue))
            {
                return new NotFoundObjectResult(new { Messages = new List<string> { "Não é possível alterar o período pois existem matriculas iniciadas." } });
            }

            Models.Onboarding onboarding = _mapper.Map<Models.Onboarding>(obj);

            IEnumerable<IGrouping<string, Enrollment>> group = onboarding.Enrollments.GroupBy(x => x.PersonalData.CPF);

            if (group.Where(x => x.ToList().Count() > 1).Count() > 0)
            {
                return new BadRequestObjectResult(new { Messages = new List<string> { "Existem cpfs duplicados neste período de matrícula." } });
            }

            foreach (Enrollment enrollment in existingOnboarding.Enrollments.ToList())
            {
                if (!onboarding.Enrollments.Any(c => c.Id == enrollment.Id))
                {
                    _context.Set<Representative>().Remove(enrollment.FinanceData.Representative);
                    _context.Set<FinanceData>().Remove(enrollment.FinanceData);
                    _context.Set<PersonalData>().Remove(enrollment.PersonalData);
                    _context.Set<Enrollment>().Remove(enrollment);
                }
            }
            foreach (Enrollment enrollment in onboarding.Enrollments)
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
                    enrollment.ExternalId = onboarding.Year + onboarding.Semester + Regex.Replace(enrollment.PersonalData.CPF, @"\D", string.Empty);
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

            onboarding.Id = existingOnboarding.Id;
            _context.Entry(existingOnboarding).CurrentValues.SetValues(onboarding);
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
                return new NotFoundObjectResult(new { Messages = new List<string> { "Esse período de matrícula não existe." } });
            }

            if (existingOnboarding.Enrollments.Any(x => x.StartedAt.HasValue))
            {
                return new NotFoundObjectResult(new { Messages = new List<string> { "Não é possível excluir o período pois existem matriculas iniciadas." } });
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
