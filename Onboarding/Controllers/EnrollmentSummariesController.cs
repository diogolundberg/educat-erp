using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using onboarding.ViewModels.EnrollmentSummaries;
using Onboarding.Controllers;
using Onboarding.Models;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EnrollmentSummariesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public EnrollmentSummariesController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/ENROLLMENTSUMMARIES/GET")]
        public dynamic Get(string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Onboarding")
                                            .Include("Pendencies")
                                            .Include("Pendencies.Section")
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
                                            .Include("PersonalData.HighSchollKind")
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
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentNumberIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            Record data = _mapper.Map<Record>(enrollment);

            return new OkObjectResult(new
            {
                data = data
            });
        }
    }
}