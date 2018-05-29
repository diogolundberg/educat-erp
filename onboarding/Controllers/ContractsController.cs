using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using onboarding.Models;
using System.Collections.Generic;
using System.Linq;
using onboarding.Services;
using onboarding.ViewModels.Contracts;
using onboarding.Validations.Contract;
using System.Collections;
using onboarding.Statuses;

namespace onboarding.Controllers
{
    public class ContractsController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly EnrollmentStepService _enrollmentStepService;
        private readonly EnrollmentService _enrollmentService;
        private readonly ContractService _contractService;

        public ContractsController(IMapper mapper, IConfiguration configuration, EnrollmentStepService enrollmentStepService, EnrollmentService enrollmentService, ContractService contractService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _enrollmentStepService = enrollmentStepService;
            _enrollmentService = enrollmentService;
            _contractService = contractService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/CONTRACTS/GET")]
        public dynamic Get(string enrollmentNumber)
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

            Contract contract = enrollment.Contract != null ? enrollment.Contract : new Contract
            {
                EnrollmentId = enrollment.Id
            };

            return Ok(_mapper.Map<Record>(contract));
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/CONTRACTS/POST")]
        public dynamic Post([FromRoute]string enrollmentNumber, [FromBody]Form obj)
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

            Contract contract = _mapper.Map<Contract>(obj);

            if (enrollment.Contract != null)
            {
                enrollment.Contract.Signature = contract.Signature;
                enrollment.Contract.AcceptedAt = contract.AcceptedAt;

                contract = _contractService.Update(enrollment.Contract);
            }
            else
            {
                contract = _contractService.Add(contract);
            }

            ContractValidator validator = new ContractValidator();
            Hashtable errors = FormatErrors(validator.Validate(contract));

            ContractStatus contractStatus = new ContractStatus(validator, contract);

            if (contractStatus.GetStatus() == "valid")
            {
                _enrollmentStepService.Update(enrollmentNumber, "Contracts");
            }

            return new OkObjectResult(new
            {
                errors,
                data = _mapper.Map<Record>(contract)
            });
        }
    }
}