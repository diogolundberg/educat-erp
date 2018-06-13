using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels.Payments;

namespace onboarding.Bindings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, Record>()
            .ForMember(x => x.Pendencies, config => config.MapFrom(x => x.EnrollmentStep.Pendencies));

            CreateMap<Form, Payment>();
        }
    }
}
