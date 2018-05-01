using AutoMapper;
using onboarding.ViewModels.Appointments;

namespace onboarding.Bindings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Models.Appointment, Record>().ReverseMap();
            CreateMap<Models.Appointment, Form>().ReverseMap();
        }
    }
}
