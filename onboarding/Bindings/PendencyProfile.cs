using AutoMapper;

namespace onboarding.Bindings
{
    public class PendencyProfile : Profile
    {
        public PendencyProfile()
        {
            CreateMap<Models.Pendency, ViewModels.Pendency>().ReverseMap();
        }
    }
}
