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

        public CourseSummariesController(DatabaseContext databaseContext, IMapper mapper, EnrollmentService enrollmentService)
        {
            _context = databaseContext;
            _mapper = mapper;
            _enrollmentService = enrollmentService;
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
    }
}