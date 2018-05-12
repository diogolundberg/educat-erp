using AutoMapper;
using finance.Models;
using finance.Validations;
using finance.ViewModels.Invoices;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace finance.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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
            return _mapper.Map<List<Records>>(_context.Invoices.Include("InvoiceItems"));
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

            dynamic billetResponseObject = GetBilletUrl(form);

            if (billetResponseObject.success != true)
            {
                return new OkObjectResult(new { billetResponseObject.messages });
            }

            invoice.Billet = billetResponseObject.url;
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

        private dynamic GetBilletUrl(Form form)
        {
            using (HttpClient client = new HttpClient())
            {
                Billet billet = new Billet
                {
                    BankCode = 237,
                    WalletNumber = "02",
                    DueDate = form.DueDate,
                    Value = form.Value,
                    DocumentNumber = "00000000000001",
                    Assignor = new Assignor
                    {
                        DocumentNumber = "103.830.576-47",
                        Name = "Lucas Costa",
                        Agency = "2222",
                        AccountBank = "222222"
                    },
                    Payer = new Payer
                    {
                        Name = "Lucas Costa",
                        Document = form.Representative is RepresentativePerson ? ((RepresentativePerson)form.Representative).Cpf : ((RepresentativeCompany)form.Representative).Cnpj,
                        Address = string.Format("{0} {1} {2} - Complemento: {3}", form.Representative.AddressKind, form.Representative.StreetAddress, form.Representative.AddressNumber, form.Representative.ComplementAddress),
                        District = "bairro",
                        City = form.Representative.City,
                        Cep = form.Representative.Zipcode,
                        State = form.Representative.State
                    }
                };

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(billet), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("http://localhost:51762/api/billets", stringContent).Result;
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}