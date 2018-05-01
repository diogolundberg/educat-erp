using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;
using onboarding.ViewModels.Appointments;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AppointmentsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public AppointmentsController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/APPOINTMENTS/LIST")]
        public dynamic GetList(string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Onboarding")
                                            .Single(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            Scheduling scheduling = _context.Schedulings
                                            .Include("Appointments")
                                            .SingleOrDefault(x => x.OnboardingId == enrollment.OnboardingId);

            if (scheduling == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.SchedulingNotExisting } });
            }

            return _mapper.Map<List<Record>>(scheduling.Appointments);
        }
    }
}