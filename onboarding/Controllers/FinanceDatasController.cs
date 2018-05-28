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

namespace onboarding.Controllers
{
    public class FinanceDatasController : BaseController<FinanceData>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly FinanceDataService _financeDataService;

        public FinanceDatasController(DatabaseContext databaseContext, IMapper mapper, FinanceDataService financeDataService)
        {
            _mapper = mapper;
            _context = databaseContext;
            _financeDataService = financeDataService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/FINANCEDATA/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
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
                data = _mapper.Map<Record>(financeData)
            });
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/FINANCEDATA/EDIT")]
        public IActionResult Update([FromRoute]string enrollmentNumber, [FromBody]Form obj)
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

            financeData = _financeDataService.Update(obj, financeData);

            FinanceDataValidator validator = new FinanceDataValidator(_context);
            FluentValidation.Results.ValidationResult results = validator.Validate(financeData);
            Hashtable errors = FormatErrors(results);

            FinanceDataMessagesValidator messagesValidator = new FinanceDataMessagesValidator(_context);
            List<string> messages = messagesValidator.Validate(financeData).Errors.Select(x => x.ErrorMessage).Distinct().ToList();

            return new OkObjectResult(new
            {
                messages,
                errors,
                data = _mapper.Map<Record>(financeData)
            });
        }
    }
}
