using AutoMapper;
using finance.Models;
using finance.ViewModels.Invoices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //[HttpPost(Name = "FINANCE/INVOICES/NEW")]
        //public dynamic Create([FromBody]Form form)
        //{
        //    Scheduling scheduling = Mapper.Map<Scheduling>(form);

        //    if (!_context.Onboardings.Any(x => x.Id == scheduling.OnboardingId))
        //    {
        //        return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingNotExisting } });
        //    }

        //    if (_context.Schedulings.Any(x => x.OnboardingId == scheduling.OnboardingId))
        //    {
        //        return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.HaveSchedulingForOnboarding } });
        //    }

        //    SchedulingValidator validator = new SchedulingValidator(_context);
        //    ValidationResult result = validator.Validate(scheduling);

        //    if (!result.IsValid)
        //    {
        //        Hashtable errors = FormatErrors(result);
        //        return new OkObjectResult(new { Errors = errors });
        //    }

        //    scheduling.Appointments = GenerateAppointmet(scheduling);

        //    _context.Schedulings.Add(scheduling);
        //    _context.SaveChanges();

        //    return new OkObjectResult(new { data = Mapper.Map<Form>(scheduling) });
        //}

        //[HttpPut("{id}", Name = "FINANCE/INVOICES/EDIT")]
        //public dynamic Edit([FromRoute]int id, [FromBody]Form form)
        //{
        //    Scheduling scheduling = Mapper.Map<Scheduling>(form);

        //    if (!_context.Onboardings.Any(x => x.Id == scheduling.OnboardingId))
        //    {
        //        return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingNotExisting } });
        //    }

        //    SchedulingValidator validator = new SchedulingValidator(_context);
        //    ValidationResult result = validator.Validate(scheduling);

        //    if (!result.IsValid)
        //    {
        //        Hashtable errors = FormatErrors(result);
        //        return new OkObjectResult(new { Errors = errors });
        //    }

        //    scheduling.Appointments = GenerateAppointmet(scheduling);

        //    _context.Schedulings.Update(scheduling);
        //    _context.SaveChanges();

        //    return new OkObjectResult(new { data = Mapper.Map<Form>(scheduling) });
        //}
    }
}