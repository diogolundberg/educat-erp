using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using onboarding.Models;
using System.Collections.Generic;
using System.Linq;
using onboarding.Services;

namespace onboarding.Controllers
{
    public class ContractsController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly EnrollmentStepService _enrollmentStepService;
        private readonly EnrollmentService _enrollmentService;

        public ContractsController(IMapper mapper, IConfiguration configuration, EnrollmentStepService enrollmentStepService, EnrollmentService enrollmentService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _enrollmentStepService = enrollmentStepService;
            _enrollmentService = enrollmentService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/CONTRACTS/GET")]
        public dynamic Get(string enrollmentNumber)
        {
            return Ok();
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/CONTRACTS/POST")]
        public dynamic Post(string enrollmentNumber)
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

            _enrollmentStepService.Update(enrollmentNumber, "Contracts");

            return Ok();
        }
    }
}