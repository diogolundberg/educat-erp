using AutoMapper;
using finance.Models;
using finance.ViewModels.Invoices;

namespace finance.Bindings
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            CreateMap<Invoice, Records>()
                .ForMember(x => x.DueDate, config => config.MapFrom(x => x.DueDate.Format()));

        }
    }
}
