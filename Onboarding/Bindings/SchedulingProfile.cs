using AutoMapper;
using Newtonsoft.Json;
using onboarding.Models;
using onboarding.ViewModels.Scheduling;

namespace onboarding.Bindings
{
    public class SchedulingProfile : Profile
    {
        public SchedulingProfile()
        {
            CreateMap<Scheduling, Form>()
                .ForMember(x => x.Breaks, config => config.MapFrom(x => x.FormBreaks));

            CreateMap<Form, Scheduling>()
                .ForMember(x => x.Breaks, config => config.MapFrom(x => JsonConvert.SerializeObject(x.Breaks)));
        }
    }
}
