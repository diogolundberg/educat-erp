using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.Services;
using onboarding.ViewModels.Approvals;
using System.Collections.Generic;

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

        [HttpGet("", Name = "ONBOARDING/APPROVALS/LIST")]
        public IActionResult List()
        {
            IEnumerable<EnrollmentStep> enrollmentSteps = _enrollmentStepService.GetForHasApprovalAndEnrollmentSentAt();
            return Ok(_mapper.Map<List<Records>>(enrollmentSteps));
        }

        [HttpGet("{id}", Name = "ONBOARDING/APPROVALS/GET")]
        public IActionResult GetById([FromRoute]int id)
        {
            EnrollmentStep enrollmentStep = _enrollmentStepService.GetById(id);

            if(enrollmentStep == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.IsEmpty } });
            }

            return Ok(_mapper.Map<Record>(enrollmentStep));
        }
    }
}