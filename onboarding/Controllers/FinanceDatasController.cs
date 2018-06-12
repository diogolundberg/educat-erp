using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;
using onboarding.ViewModels.FinanceDatas;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using onboarding.Validations;
using onboarding.Validations.FinanceData;
using onboarding.ViewModels;
using onboarding.Services;
using onboarding.Statuses;

namespace onboarding.Controllers
{
    public class FinanceDatasController : BaseController<FinanceData>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly FinanceDataService _financeDataService;
        private readonly EnrollmentStepService _enrollmentStepService;

        public FinanceDatasController(DatabaseContext databaseContext, IMapper mapper, FinanceDataService financeDataService, EnrollmentStepService enrollmentStepService)
        {
            _mapper = mapper;
            _context = databaseContext;
            _financeDataService = financeDataService;
            _enrollmentStepService = enrollmentStepService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/FINANCEDATA/GET")]
        public IActionResult Get([FromRoute]string enrollmentNumber)
        {
            FinanceData financeData = _financeDataService.List().SingleOrDefault(x => x.Enrollment.ExternalId == enrollmentNumber);

            if (financeData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!financeData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            return new OkObjectResult(new
            {
                errors = financeData.UpdatedAt.HasValue ? FormatErrors((new FinanceDataValidator(_context)).Validate(financeData)) : new Hashtable(),
                messages = financeData.UpdatedAt.HasValue ? ((new FinanceDataMessagesValidator(_context)).Validate(financeData)).Errors.Select(x => x.ErrorMessage).Distinct().ToList() : new List<string>(),
                data = _mapper.Map<Record>(financeData)
            });
        }

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/FINANCEDATA/CREATE")]
        public IActionResult Put([FromRoute]string enrollmentNumber, [FromBody]Form obj)
        {
            FinanceData financeData = _financeDataService.List().SingleOrDefault(x => x.Enrollment.ExternalId == enrollmentNumber);

            if (financeData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!financeData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            if (!financeData.Editable)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentInReview } });
            }

            FinanceDataValidator validator = new FinanceDataValidator(_context);
            FinanceDataMessagesValidator messagesValidator = new FinanceDataMessagesValidator(_context);

            financeData = _financeDataService.Update(obj, financeData);

            return new OkObjectResult(new
            {
                messages = messagesValidator.Validate(financeData).Errors.Select(x => x.ErrorMessage).Distinct().ToList(),
                errors = FormatErrors(validator.Validate(financeData)),
                data = _mapper.Map<Record>(financeData)
            });
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/FINANCEDATA/EDIT")]
        public IActionResult Post([FromRoute]string enrollmentNumber, [FromBody]Form obj)
        {
            FinanceData financeData = _financeDataService.List().SingleOrDefault(x => x.Enrollment.ExternalId == enrollmentNumber);

            if (financeData == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!financeData.Enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            if (!financeData.Editable)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentInReview } });
            }

            FinanceDataValidator validator = new FinanceDataValidator(_context);
            FinanceDataMessagesValidator messagesValidator = new FinanceDataMessagesValidator(_context);

            if ((new FinanceDataStatus(validator, financeData, messagesValidator)).GetStatus() == "valid")
            {
                financeData.EnrollmentStepId = _enrollmentStepService.Update(enrollmentNumber, "FinanceDatas");
                _financeDataService.Update(financeData);
            }

            return new OkObjectResult(new
            {
                messages = messagesValidator.Validate(financeData).Errors.Select(x => x.ErrorMessage).Distinct().ToList(),
                errors = FormatErrors(validator.Validate(financeData)),
                data = _mapper.Map<Record>(financeData)
            });
        }
    }
}
