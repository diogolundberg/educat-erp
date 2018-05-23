using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using onboarding.Models;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
{
    public class FinancePendenciesController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public FinancePendenciesController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpDelete("{enrollmentNumber}", Name = "ONBOARDING/FINANCEPENDENCIES/DELETE")]
        public dynamic Edit(string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments.Include("Onboarding").Include("Pendencies").Single(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            _context.Set<FinancePendency>().RemoveRange(enrollment.FinancePendencies);
            _context.SaveChanges();

            return Ok();
        }
    }
}