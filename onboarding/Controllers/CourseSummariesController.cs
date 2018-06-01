using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.Services;
using System.Collections.Generic;
using System.Linq;
using onboarding.ViewModels.CourseSummaries;

namespace onboarding.Controllers
{
    public class CourseSummariesController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly EnrollmentService _enrollmentService;
        private readonly EnrollmentStepService _enrollmentStepService;

        public CourseSummariesController(DatabaseContext databaseContext, IMapper mapper, EnrollmentService enrollmentService, EnrollmentStepService enrollmentStepService)
        {
            _context = databaseContext;
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _enrollmentStepService = enrollmentStepService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/COURSESUMMARIES/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _enrollmentService.List().SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            return new OkObjectResult(new
            {
                data = _mapper.Map<Record>(enrollment)
            });
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/COURSESUMMARIES/EDIT")]
        public IActionResult Post([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _enrollmentService.List().SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            _enrollmentStepService.Update(enrollmentNumber, "CourseSummaries");

            return new OkObjectResult(new
            {
                data = _mapper.Map<Record>(enrollment)
            });
        }
    }
}