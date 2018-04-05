using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Validations.Onboarding;

namespace Onboarding.Controllers
{
    [Route("api/[controller]")]
    public class OnboardingController : BaseController
    {
        [HttpPost("", Name = "ONBOARDING/CREATE")]
        public dynamic Post([FromBody]ViewModels.Onboarding.Form obj)
        {
            FormValidator formValidator = new FormValidator();
            ValidationResult validationResult = formValidator.Validate(obj);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new { Errors = FormatErrors(validationResult) });
            }

            return new OkObjectResult(new { data = obj });
        }
    }
}
