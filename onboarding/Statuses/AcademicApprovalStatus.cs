using FluentValidation;
using onboarding.Models;
using System.Linq;

namespace onboarding.Statuses
{
    public class AcademicApprovalStatus : BaseStatus<Enrollment>
    {
        public AcademicApprovalStatus(AbstractValidator<Enrollment> validator, Enrollment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (_entity.AcademicApproval.HasValue)
            {
                return "approved";
            }
            else if (_entity.AcademicPendencies.Count() > 0)
            {
                return "pending";
            }
            else
            {
                return "inReview";
            }
        }

        public string GetStatusName()
        {
            string status = GetStatus();

            switch (status)
            {
                case "approved":
                    return "Aprovado";
                case "pending":
                    return "Pendente";
                default:
                    return "Em revisão";
            }
        }
    }
}
