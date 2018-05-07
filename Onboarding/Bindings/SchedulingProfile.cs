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
            CreateMap<Scheduling, Records>()
                .ForMember(x => x.StartAt, config => config.MapFrom(x => x.StartAt.Format()))
                .ForMember(x => x.EndAt, config => config.MapFrom(x => x.EndAt.Format()))
                .ForMember(x => x.Onboarding, config => config.MapFrom(x => x.Onboarding.Semester + "/" + x.Onboarding.Year));

            CreateMap<Scheduling, Record>()
                .ForMember(x => x.StartAt, config => config.MapFrom(x => x.StartAt.Format()))
                .ForMember(x => x.EndAt, config => config.MapFrom(x => x.EndAt.Format()))
                .ForMember(x => x.Onboarding, config => config.MapFrom(x => x.Onboarding.Semester + "/" + x.Onboarding.Year))
                .ForMember(x => x.Breaks, config => config.MapFrom(x => x.DeserializedBreaks));

            CreateMap<Scheduling, Form>()
                .ForMember(x => x.StartAt, config => config.MapFrom(x => x.StartAt.Format()))
                .ForMember(x => x.EndAt, config => config.MapFrom(x => x.EndAt.Format()))
                .ForMember(x => x.Breaks, config => config.MapFrom(x => x.DeserializedBreaks));

            CreateMap<Form, Scheduling>()
                .ForMember(x => x.Breaks, config => config.MapFrom(x => JsonConvert.SerializeObject(x.Breaks)));
        }
    }
}
