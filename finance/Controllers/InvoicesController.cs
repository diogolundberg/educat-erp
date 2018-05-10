using AutoMapper;
using finance.Models;
using finance.ViewModels.invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace finance.Controllers
{
    public class InvoicesController : BaseController 
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public InvoicesController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("", Name = "FINANCE/INVOICES/LIST")]
        public dynamic GetList()
        {
            return _mapper.Map<List<Records>>(_context.Invoices.Include("InvoiceItens"));
        }

    }
}