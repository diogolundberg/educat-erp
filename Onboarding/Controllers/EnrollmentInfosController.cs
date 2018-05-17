using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
{
    public class EnrollmentInfosController : BaseController<Enrollment>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public EnrollmentInfosController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/ENROLLMENTINFOS/POST")]
        public dynamic Post(string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments.Include("Onboarding").Single(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            enrollment.EnrollmentInfo = DateTime.Now;

            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();

            return Ok();
        }
    }
}