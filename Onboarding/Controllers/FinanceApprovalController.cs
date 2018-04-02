
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;
using Onboarding.ViewModels.FinanceApprovals;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FinanceApprovalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public FinanceApprovalController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("", Name = "ONBOARDING/FINANCEAPPROVAL/LIST")]
        public dynamic GetList()
        {
            List<Enrollment> enrollments = _context.Enrollments.Include("PersonalData").Where(x => x.SentAt.HasValue).ToList();
            List<Records> records = _mapper.Map<List<Records>>(enrollments);
            return new { records };
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/FINANCEAPPROVAL/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments.SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            Record data = _mapper.Map<Record>(enrollment); // TODO: Adicionar todos os dados para a visualização

            return new OkObjectResult(new { data });
        }

        [HttpPost("", Name = "ONBOARDING/FINANCEAPPROVAL/NEW")]
        public dynamic Create([FromBody]Form form)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Pendencies")
                                            .SingleOrDefault(x => x.ExternalId == form.EnrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            if (!ModelState.IsValid)
            {
                var errors = new Hashtable();
                Dictionary<string, string[]> modelStateErrors = ModelState.ToDictionary(
                    modelState => modelState.Key.UnCapitalize(),
                    modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                foreach (var error in modelStateErrors)
                {
                    string[] split = error.Key.Split('.');

                    if (split.Length == 1)
                    {
                        errors.Add(error.Key, error.Value);
                    }
                    else
                    {
                        if (!errors.ContainsKey(split[0]))
                        {
                            errors.Add(split[0], new Hashtable());
                        }

                        ((Hashtable)errors[split[0]]).Add(split[1].UnCapitalize(), error.Value);
                    }
                }

                return new BadRequestObjectResult(new { errors });
            }

            foreach (Models.Pendency pendency in enrollment.Pendencies.ToList())
            {
                if (!form.FinancePendencies.Any(c => c.Id == pendency.Id))
                {
                    if (pendency is Models.FinancePendency)
                    {
                        _context.Set<Models.Pendency>().Remove(pendency);
                    }
                }
            }
            foreach (ViewModels.Pendency pendency in form.FinancePendencies)
            {
                Models.Pendency existingPendency = enrollment.Pendencies.Where(c => c.Id == pendency.Id).SingleOrDefault();

                if (existingPendency == null)
                {
                    _context.Set<Models.FinancePendency>().Add(new Models.FinancePendency
                    {
                        SectionId = pendency.SectionId.Value,
                        Description = pendency.Description,
                        EnrollmentId = enrollment.Id
                    });
                }
                else
                {
                    existingPendency.Description = pendency.Description;
                    existingPendency.SectionId = pendency.SectionId.Value;

                    _context.Set<Models.Pendency>().Update(existingPendency);
                }
            }

            if (form.FinancePendencies.Count() == 0)
            {
                enrollment.FinanceApproval = DateTime.Now;
            }

            if ((enrollment.AcademicApproval.HasValue || enrollment.Pendencies.OfType<AcademicPendency>().Count() > 0)
                && (enrollment.FinanceApproval.HasValue || form.FinancePendencies.Count() > 0))
            {
                enrollment.ReviewedAt = DateTime.Now;
            }

            if (enrollment.ReviewedAt.HasValue && (form.FinancePendencies.Count() > 0 || enrollment.Pendencies.OfType<AcademicPendency>().Count() > 0))
            {
                enrollment.SentAt = null;
            }

            List<string> messages = new List<string>();

            _context.Set<Enrollment>().Update(enrollment);

            _context.SaveChanges();

            messages.Add("Procedimento realizado com sucesso.");

            return new OkObjectResult(new { messages });
        }
    }
}
