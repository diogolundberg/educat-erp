using AutoMapper;
using finance.Models;
using finance.ViewModels.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace finance.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public PaymentsController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpPut("{invoiceId}", Name = "FINANCE/PAYMENTS/EDIT")]
        public dynamic Create([FromRoute]int invoiceId)
        {
            Invoice obj = _context.Invoices.Include("InvoiceItems").SingleOrDefault(x => x.Id == invoiceId);

            if (obj == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { finance.Resources.Messages.InvoiceNotExisting } });
            }
            if (obj.CompensatedDate.HasValue)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { finance.Resources.Messages.InvoiceCompensated } });
            }

            obj.CompensatedDate = DateTime.Now;

            _context.Invoices.Update(obj);
            _context.SaveChanges();

            return new OkObjectResult(new { data = Mapper.Map<Form>(obj) });
        }
    }
}