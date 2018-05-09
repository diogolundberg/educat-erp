using AutoMapper;
using onboarding.ViewModels.Appointments;

namespace onboarding.Bindings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Models.Appointment, Record>()
            .ForMember(x => x.Scheduled, config => config.MapFrom(x => x.EnrollmentId.HasValue));

            CreateMap<Models.Appointment, Form>().ReverseMap();
        }
    }
}
