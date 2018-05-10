﻿using AutoMapper;
using finance.Models;
using finance.ViewModels.Invoices;

namespace finance.Bindings
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            CreateMap<InvoiceItem, Item>().ReverseMap();

            CreateMap<Invoice, Records>()
                .ForMember(x => x.DueDate, config => config.MapFrom(x => x.DueDate.Format()));

            CreateMap<Invoice, Record>()
                .ForMember(x => x.DueDate, config => config.MapFrom(x => x.DueDate.Format()))
                .ForMember(x => x.Items, config => config.MapFrom(x => x.InvoiceItens));

        }
    }
}