using AutoMapper;
using finance.Models;
using finance.ViewModels.Invoices;

namespace finance.Bindings
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            CreateMap<InvoiceItem, Item>().ReverseMap();

            CreateMap<Invoice, Records>().ReverseMap();

            CreateMap<Invoice, Record>()
                .ForMember(x => x.Items, config => config.MapFrom(x => x.InvoiceItems));

            CreateMap<Invoice, Form>()
                .ForMember(x => x.Items, config => config.MapFrom(x => x.InvoiceItems));

            CreateMap<Form, Invoice>()
                .ForMember(x => x.InvoiceItems, config => config.MapFrom(x => x.Items));
        }
    }
}
