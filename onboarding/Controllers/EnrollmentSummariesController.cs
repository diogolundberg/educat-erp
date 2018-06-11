using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using onboarding.ViewModels.EnrollmentSummaries;
using onboarding.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using onboarding.Services;

namespace onboarding.Controllers
{
    public class EnrollmentSummariesController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly EnrollmentStepService _enrollmentStepService;
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentSummariesController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration, EnrollmentStepService enrollmentStepService, EnrollmentService enrollmentService)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
            _enrollmentStepService = enrollmentStepService;
            _enrollmentService = enrollmentService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/ENROLLMENTSUMMARIES/GET")]
        public IActionResult Get([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _enrollmentService.List()
                                            .Include("Onboarding")
                                            .Include("PersonalData")
                                            .Include("PersonalData.Gender")
                                            .Include("PersonalData.MaritalStatus")
                                            .Include("PersonalData.BirthCity")
                                            .Include("PersonalData.BirthState")
                                            .Include("PersonalData.BirthCountry")
                                            .Include("PersonalData.HighSchoolGraduationCountry")
                                            .Include("PersonalData.City")
                                            .Include("PersonalData.State")
                                            .Include("PersonalData.AddressKind")
                                            .Include("PersonalData.Race")
                                            .Include("PersonalData.HighSchoolKind")
                                            .Include("PersonalData.Nationality")
                                            .Include("PersonalData.PersonalDataDisabilities.Disability")
                                            .Include("PersonalData.PersonalDataSpecialNeeds.SpecialNeed")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("PersonalData.PersonalDataDocuments.Document.DocumentType")
                                            .Include("FinanceData")
                                            .Include("FinanceData.Plan")
                                            .Include("FinanceData.Representative")
                                            .Include("FinanceData.Guarantors")
                                            .Include("FinanceData.Guarantors.Relationship")
                                            .Include("FinanceData.PaymentMethod")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments")
                                            .Include("FinanceData.Guarantors.AddressKind")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document.DocumentType")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentNumberIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            if (enrollment.FinanceData.Representative is Models.RepresentativePerson)
            {
                _context.Entry((Models.RepresentativePerson)enrollment.FinanceData.Representative).Reference(x => x.Relationship).Load();
            }

            Record data = _mapper.Map<Record>(enrollment);

            return new OkObjectResult(new
            {
                data
            });
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/ENROLLMENTSUMMARIES/CREATE")]
        public IActionResult Post([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments.Include("Onboarding").Single(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            enrollment.SentAt = DateTime.Now;
            _enrollmentService.Update(enrollment);

            _enrollmentStepService.Update(enrollmentNumber, "EnrollmentSummaries");

            return Ok();
        }
    }
}