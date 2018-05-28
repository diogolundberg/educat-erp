using AutoMapper;
using onboarding.Models;
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
            return _context.Set<Models.Step>().OrderBy(x => x.Order).Select(x => new ViewModels.Enrollments.Step
            {
                Resource = x.Resource,
                Name = x.Name,
                Status = "false"
            }).ToList();
        }
    }
}
