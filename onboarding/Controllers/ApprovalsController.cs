using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.Services;
using onboarding.Validations.Approval;
using onboarding.ViewModels.Approvals;
using System.Collections.Generic;

namespace onboarding.Controllers
{
    public class ApprovalsController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly EnrollmentStepService _enrollmentStepService;
        private readonly SectionService _sectionService;

        public ApprovalsController(IMapper mapper, EnrollmentStepService enrollmentStepService, SectionService sectionService)
        {
            _mapper = mapper;
            _enrollmentStepService = enrollmentStepService;
            _sectionService = sectionService;
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

            if (enrollmentStep == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.IsEmpty } });
            }

            return Ok(_mapper.Map<Record>(enrollmentStep));
        }

        [HttpPost("{id}", Name = "ONBOARDING/APPROVALS/POST")]
        public IActionResult Post([FromRoute]int id, [FromBody]Form obj)
        {
            EnrollmentStep enrollmentStep = _enrollmentStepService.GetById(id);

            if (enrollmentStep == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.IsEmpty } });
            }

            EnrollmentStepValidator validator = new EnrollmentStepValidator();
            ValidationResult validationResult = validator.Validate(_mapper.Map<EnrollmentStep>(obj));

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new { errors = FormatErrors(validationResult) });
            }

            enrollmentStep = _enrollmentStepService.UpdatePendencies(enrollmentStep, _mapper.Map<List<Pendency>>(obj.Pendencies));

            return Ok(_mapper.Map<Record>(enrollmentStep));
        }

        [HttpOptions(Name = "ONBOARDING/APPROVALS/OPTIONS")]
        public IActionResult Options()
        {
            return Ok(_sectionService.List());
        }
    }
}