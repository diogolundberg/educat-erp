using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using onboarding.Models;
using onboarding.Services;
using onboarding.ViewModels;
using onboarding.ViewModels.FinanceDatas;
using onboarding.ViewModels.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace onboarding.Controllers
{
    public class PaymentsController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly EnrollmentStepService _enrollmentStepService;
        private readonly EnrollmentService _enrollmentService;
        private readonly PaymentService _paymentService;

        public PaymentsController(IConfiguration configuration, IMapper mapper, DatabaseContext context, EnrollmentStepService enrollmentStepService, EnrollmentService enrollmentService, PaymentService paymentService)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _enrollmentStepService = enrollmentStepService;
            _enrollmentService = enrollmentService;
            _paymentService = paymentService;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/PAYMENTS/GET")]
        public IActionResult Get([FromRoute]string enrollmentNumber)
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

            if(enrollment.Payment == null || !enrollment.Payment.InvoiceNumber.HasValue)
            {
                _paymentService.Create(enrollment);
            }

            return new OkObjectResult(new { data = _mapper.Map<ViewModels.Payments.Record>(enrollment.Payment) });
        }

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/PAYMENTS/EDIT")]
        public IActionResult Put([FromRoute]string enrollmentNumber)
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

            return Ok();
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/PAYMENTS/CREATE")]
        public IActionResult Post([FromRoute]string enrollmentNumber)
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

            //_enrollmentStepService.Update(enrollmentNumber, "Payments");

            return Ok();
        }
    }
}