using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;
using onboarding.ViewModels.Avatar;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
{
    public class AvatarController : BaseController<Enrollment>
    {
        private readonly DatabaseContext _context;

        public AvatarController(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        [HttpPost("{enrollmentNumber}", Name = "ONBOARDING/AVATAR")]
        public dynamic Post([FromRoute]string enrollmentNumber, [FromBody]Form form)
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

            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new { errors = GetErrors() });
            }

            enrollment.Photo = form.photo;
            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();

            return new OkObjectResult(new { data = form });
        }
    }
}