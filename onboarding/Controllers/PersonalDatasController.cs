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

namespace onboarding.Controllers
{
    public class PersonalDatasController : BaseController<PersonalData>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly PersonalDataService _personalDataService;

        public PersonalDatasController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper, PersonalDataService personalDataService)
        {
            _context = databaseContext;
            _mapper = mapper;
            _personalDataService = personalDataService;
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

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update([FromRoute]string enrollmentNumber, [FromBody]Form obj)
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
    }
}
