using AutoMapper;
using onboarding.Models;
using onboarding.Statuses;
using onboarding.ViewModels.Enrollments;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Resolvers
{
    public class StepResolver : IValueResolver<Enrollment, Record, IEnumerable<ViewModels.Enrollments.Step>>
    {
        private readonly DatabaseContext _context;

        public StepResolver(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ViewModels.Enrollments.Step> Resolve(Enrollment source, Record destination, IEnumerable<ViewModels.Enrollments.Step> destMember, ResolutionContext context)
        {
            List<ViewModels.Enrollments.Step> steps = new List<ViewModels.Enrollments.Step>();

            foreach (Models.Step step in _context.Set<Models.Step>().OrderBy(x => x.Order))
            {
                EnrollmentStep enrollmentStep = source.EnrollmentSteps.FirstOrDefault(x => x.StepId == step.Id);

                steps.Add(new ViewModels.Enrollments.Step
                {
                    Resource = step.Resource,
                    Name = step.Name,
                    Status = new StepStatus(null, step, enrollmentStep).GetStatus(),
                    Pendencies = enrollmentStep != null ? enrollmentStep.Pendencies.Select(x => new ViewModels.Pendency
                    {
                        Anchor = x.Section.Anchor,
                        Description = x.Description,
                        Id = x.Id,
                        SectionId = x.SectionId
                    }) : new List<ViewModels.Pendency>()
                });
            }

            return steps;
        }
    }
}