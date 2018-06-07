using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using onboarding.Models;
using onboarding.Validations.PersonalData;
using onboarding.ViewModels.PersonalDatas;
using onboarding.Services;
using onboarding.Statuses;

namespace onboarding.Controllers
{
    public class PersonalDatasController : BaseController<PersonalData>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly PersonalDataService _personalDataService;
        private readonly EnrollmentStepService _enrollmentStepService;

        public PersonalDatasController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper, PersonalDataService personalDataService, EnrollmentStepService enrollmentStepService)
        {
            _context = databaseContext;
            _mapper = mapper;
            _personalDataService = personalDataService;
            _enrollmentStepService = enrollmentStepService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/PERSONALDATA/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
        {
            PersonalData personalData = _personalDataService.List().SingleOrDefault(x => x.Enrollment.ExternalId == enrollmentNumber);

            if (personalData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!personalData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            return new OkObjectResult(new
            {
                data = _mapper.Map<Record>(personalData)
            });
        }

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/PERSONALDATA/CREATE")]
        public IActionResult Put([FromRoute]string enrollmentNumber, [FromBody]Form obj)
        {
            PersonalData personalData = _personalDataService.List().SingleOrDefault(x => x.Enrollment.ExternalId == enrollmentNumber);

            if (personalData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!personalData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            if (!personalData.Editable)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentInReview } });
            }

            PersonalData personalDataMapped = _mapper.Map<PersonalData>(obj);
            personalData = _personalDataService.Update(personalData, personalDataMapped);

            PersonalDataValidator validator = new PersonalDataValidator(_context);
            Hashtable errors = FormatErrors(validator.Validate(personalData));

            return new OkObjectResult(new
            {
                errors,
                data = _mapper.Map<Record>(personalData)
            });
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Post([FromRoute]string enrollmentNumber)
        {
            PersonalData personalData = _personalDataService.List().SingleOrDefault(x => x.Enrollment.ExternalId == enrollmentNumber);

            if (personalData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!personalData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            if (!personalData.Editable)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentInReview } });
            }

            PersonalDataValidator validator = new PersonalDataValidator(_context);
            Hashtable errors = FormatErrors(validator.Validate(personalData));
            PersonalDataStatus personalDataStatus = new PersonalDataStatus(validator, personalData);

            if (personalDataStatus.GetStatus() == "valid")
            {
                _enrollmentStepService.Update(enrollmentNumber, "PersonalDatas");
            }

            return new OkObjectResult(new
            {
                errors,
                data = _mapper.Map<Record>(personalData)
            });
        }
    }
}
