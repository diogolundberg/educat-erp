using Microsoft.EntityFrameworkCore;
using onboarding.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Services
{
    public class EnrollmentStepService : BaseService<EnrollmentStep>
    {
        public EnrollmentStepService(DatabaseContext context) : base(context)
        {
        }

        public override IQueryable<EnrollmentStep> List()
        {
            return base.List().Include("Enrollment").Include("Step");
        }

        public void Update(string enrollmentNumber, string resource)
        {
            Enrollment enrollment = _context.Enrollments.Include("EnrollmentSteps").Include("EnrollmentSteps.Step").SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment != null)
            {
                EnrollmentStep enrollmentStep = enrollment.EnrollmentSteps.FirstOrDefault(x => x.Step.Resource == resource);

                if (enrollmentStep == null)
                {
                    Step step = _context.Steps.SingleOrDefault(x => x.Resource == resource);

                    if (step != null)
                    {
                        Add(new EnrollmentStep { StepId = step.Id, EnrollmentId = enrollment.Id });
                    }
                }
            }
        }

        public IEnumerable<EnrollmentStep> GetForHasApprovalAndEnrollmentSentAt()
        {
            return List()
                .Include("Enrollment.PersonalData")
                .Where(x => x.Step.HasApproval && x.Enrollment.SentAt.HasValue).OrderBy(x => x.Enrollment.SentAt)
                .ToList();
        }
    }
}
