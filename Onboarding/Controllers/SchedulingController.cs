using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nager.Date;
using onboarding.Models;
using onboarding.Validations.Scheduling;
using onboarding.ViewModels.Scheduling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SchedulingController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public SchedulingController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("", Name = "ONBOARDING/SCHEDULING/LIST")]
        public dynamic GetList()
        {
            return _mapper.Map<List<Records>>(_context.Schedulings.Include("Onboarding"));
        }

        [HttpGet("{id}", Name = "ONBOARDING/SCHEDULING/GET")]
        public dynamic GetById([FromRoute]int id)
        {
            Scheduling obj = _context.Schedulings.Include("Onboarding").SingleOrDefault(x => x.Id == id);

            if (obj == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.SchedulingNotExisting } });
            }

            return _mapper.Map<Record>(obj);
        }

        [HttpPost(Name = "ONBOARDING/SCHEDULING/NEW")]
        public dynamic Create([FromBody]Form form)
        {
            Scheduling scheduling = Mapper.Map<Scheduling>(form);

            if (!_context.Onboardings.Any(x => x.Id == scheduling.OnboardingId))
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingNotExisting } });
            }

            if (_context.Schedulings.Any(x => x.OnboardingId == scheduling.OnboardingId))
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.HaveSchedulingForOnboarding } });
            }

            SchedulingValidator validator = new SchedulingValidator(_context);
            ValidationResult result = validator.Validate(scheduling);

            if (!result.IsValid)
            {
                Hashtable errors = FormatErrors(result);
                return new OkObjectResult(new { Errors = errors });
            }

            for (int i = 0; i < (scheduling.EndAt - scheduling.StartAt).Days; i++)
            {
                DateTime referenceDate = scheduling.StartAt.AddDays(i);

                if (!DateSystem.IsPublicHoliday(referenceDate, CountryCode.BR) && referenceDate.DayOfWeek != DayOfWeek.Saturday && referenceDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    DateTime appointmentDate = referenceDate.Date;

                    while (referenceDate.Date == appointmentDate.Date)
                    {
                        scheduling.Appointments.Add(new Appointment
                        {
                            Hour = appointmentDate.ToString("HH:mm"),
                            Date = referenceDate.ToString("dd/MM/yyyy")
                        });
                        appointmentDate = appointmentDate.AddMinutes(int.Parse(scheduling.Intervals));
                    }
                }
            }

            _context.Schedulings.Add(scheduling);
            _context.SaveChanges();

            return new OkObjectResult(new { data = Mapper.Map<Form>(scheduling) });
        }

        [HttpPut("{id}", Name = "ONBOARDING/SCHEDULING/EDIT")]
        public dynamic Edit([FromRoute]int id, [FromBody]Form form)
        {
            Scheduling scheduling = Mapper.Map<Scheduling>(form);

            if (!_context.Onboardings.Any(x => x.Id == scheduling.OnboardingId))
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingNotExisting } });
            }

            SchedulingValidator validator = new SchedulingValidator(_context);
            ValidationResult result = validator.Validate(scheduling);

            if (!result.IsValid)
            {
                Hashtable errors = FormatErrors(result);
                return new OkObjectResult(new { Errors = errors });
            }

            _context.Schedulings.Update(scheduling);
            _context.SaveChanges();

            return new OkObjectResult(new { data = Mapper.Map<Form>(scheduling) });
        }
    }
}