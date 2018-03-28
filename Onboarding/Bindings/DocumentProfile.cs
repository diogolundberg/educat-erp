using AutoMapper;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Bindings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentViewModel>().ReverseMap();
        }
    }
}
