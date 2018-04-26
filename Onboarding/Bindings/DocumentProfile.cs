using AutoMapper;
using onboarding.Models;
using onboarding.ViewModels;

namespace onboarding.Bindings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentViewModel>().ReverseMap();
        }
    }
}
