using AutoMapper;
using finance.Models;
using finance.Validations;
using finance.ViewModels.Invoices;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("{id}", Name = "FINANCE/INVOICES/GET")]
        public dynamic GetById([FromRoute]int id)
        {
            Invoice obj = _context.Invoices.Include("InvoiceItems").SingleOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { finance.Resources.Messages.InvoiceNotExisting } });
            }

            return _mapper.Map<Record>(obj);
        }

        [HttpPost(Name = "FINANCE/INVOICES/NEW")]
        public dynamic Create([FromBody]Form form)
        {
            Invoice invoice = Mapper.Map<Invoice>(form);

            InvoiceValidator validator = new InvoiceValidator(_context);
            ValidationResult result = validator.Validate(invoice);

            if (!result.IsValid)
            {
                Hashtable errors = FormatErrors(result);
                return new OkObjectResult(new { Errors = errors });
            }

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return new OkObjectResult(new { data = Mapper.Map<Form>(invoice) });
        }

        [HttpPut("{id}", Name = "FINANCE/INVOICES/EDIT")]
        public dynamic Edit([FromRoute]int id, [FromBody]Form form)
        {
            Invoice invoice = Mapper.Map<Invoice>(form);

            if (!_context.Invoices.Any(x => x.Id == invoice.Id))
            {
                return new BadRequestObjectResult(new { messages = new List<string> { finance.Resources.Messages.InvoiceNotExisting } });
            }

            InvoiceValidator validator = new InvoiceValidator(_context);
            ValidationResult result = validator.Validate(invoice);

            if (!result.IsValid)
            {
                Hashtable errors = FormatErrors(result);
                return new OkObjectResult(new { Errors = errors });
            }

            _context.Invoices.Update(invoice);
            _context.SaveChanges();

            return new OkObjectResult(new { data = Mapper.Map<Form>(invoice) });
        }
    }
}