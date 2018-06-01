using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.Services;

namespace onboarding.Controllers
{
    public class ApprovalsController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly EnrollmentStepService _enrollmentStepService;

        public ApprovalsController(IMapper mapper, EnrollmentStepService enrollmentStepService)
        {
            _mapper = mapper;
            _enrollmentStepService = enrollmentStepService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/APPROVALS/LIST")]
        public IActionResult List()
        {
            return Ok();
        }
    }
}