using AutoMapper;
using finance.Models;
using finance.ViewModels.invoices;

namespace finance.Bindings
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            CreateMap<Invoice, Records>().ReverseMap();
        }
    }
}
