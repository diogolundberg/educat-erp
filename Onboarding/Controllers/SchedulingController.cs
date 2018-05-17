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
    public class SchedulingController : BaseController<Scheduling>
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

            scheduling.Appointments = GenerateAppointmet(scheduling);

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

            scheduling.Appointments = GenerateAppointmet(scheduling);

            _context.Schedulings.Update(scheduling);
            _context.SaveChanges();

            return new OkObjectResult(new { data = Mapper.Map<Form>(scheduling) });
        }

        private List<Appointment> GenerateAppointmet(Scheduling scheduling)
        {
            List<Appointment> appointments = new List<Appointment>();

            for (int i = 0; i < (scheduling.EndAt - scheduling.StartAt).Days; i++)
            {
                DateTime referenceDate = scheduling.StartAt.AddDays(i);

                if (!DateSystem.IsPublicHoliday(referenceDate, CountryCode.BR) && referenceDate.DayOfWeek != DayOfWeek.Saturday && referenceDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    DateTime appointmentDate = referenceDate.Date
                                                            .AddHours(int.Parse(scheduling.ScheduleStartTime.Split(":")[0]))
                                                            .AddMinutes(int.Parse(scheduling.ScheduleStartTime.Split(":")[1]));

                    while (referenceDate.Date == appointmentDate.Date)
                    {
                        DateTime endDateTime = appointmentDate.Date
                                                              .AddHours(int.Parse(scheduling.ScheduleEndTime.Split(":")[0]))
                                                              .AddMinutes(int.Parse(scheduling.ScheduleEndTime.Split(":")[1]));

                        if (appointmentDate < endDateTime && !IsBlockTime(scheduling, appointmentDate))
                        {
                            appointments.Add(new Appointment
                            {
                                Hour = appointmentDate.ToString("HH:mm"),
                                Date = referenceDate.ToString("dd/MM/yyyy")
                            });
                        }

                        appointmentDate = appointmentDate.AddMinutes(int.Parse(scheduling.Intervals));
                    }
                }
            }

            return appointments;
        }

        private bool IsBlockTime(Scheduling scheduling, DateTime appointmentDate)
        {
            List<dynamic> breaks = new List<dynamic>();

            foreach (FormBreak formBreak in scheduling.DeserializedBreaks)
            {
                breaks.Add(new
                {
                    start = appointmentDate.Date
                                           .AddHours(int.Parse(formBreak.Start.Split(":")[0]))
                                           .AddMinutes(int.Parse(formBreak.Start.Split(":")[1])),
                    end = appointmentDate.Date
                                         .AddHours(int.Parse(formBreak.End.Split(":")[0]))
                                         .AddMinutes(int.Parse(formBreak.End.Split(":")[1])),
                });
            }

            return breaks.Any(x => appointmentDate >= x.start && appointmentDate < x.end);
        }
    }
}