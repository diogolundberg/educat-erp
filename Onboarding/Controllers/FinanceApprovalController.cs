
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;
using Onboarding.Statuses;
using Onboarding.Validations;
using Onboarding.Validations.FinanceData;
using Onboarding.ViewModels.FinanceApprovals;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FinanceApprovalController : BaseController
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
            List<Enrollment> enrollments = _context.Enrollments
                                                    .Include("Onboarding")
                                                    .Include("Pendencies")
                                                    .Include("Pendencies.Section")
                                                    .Include("PersonalData")
                                                    .Include("FinanceData")
                                                    .Include("FinanceData.Plan")
                                                    .Include("FinanceData.Representative")
                                                    .Include("FinanceData.Guarantors")
                                                    .Include("FinanceData.Guarantors.Relationship")
                                                    .Include("FinanceData.Guarantors.GuarantorDocuments")
                                                    .Include("FinanceData.Guarantors.GuarantorDocuments.Document")
                                                    .Where(x => x.SentAt.HasValue && !x.FinanceApproval.HasValue)
                                                    .ToList();

            List<Records> records = _mapper.Map<List<Records>>(enrollments);

            foreach (Records record in records)
            {
                Enrollment enrollment = enrollments.Single(x => x.ExternalId == record.EnrollmentNumber);
                record.Status = record.State = (new FinanceApprovalStatus(null, enrollment)).GetStatus();
            }

            return new { records };
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/FINANCEAPPROVAL/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("PersonalData")
                                            .Include("Pendencies")
                                            .Include("Pendencies.Section")
                                            .Include("FinanceData")
                                            .Include("FinanceData.Plan")
                                            .Include("FinanceData.PaymentMethod")
                                            .Include("FinanceData.Representative")
                                            .Include("FinanceData.Representative.City")
                                            .Include("FinanceData.Representative.State")
                                            .Include("FinanceData.Representative.AddressKind")
                                            .Include("FinanceData.Guarantors")
                                            .Include("FinanceData.Guarantors.City")
                                            .Include("FinanceData.Guarantors.State")
                                            .Include("FinanceData.Guarantors.AddressKind")
                                            .Include("FinanceData.Guarantors.Relationship")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document")
                                            .Include("FinanceData.Guarantors.GuarantorDocuments.Document.DocumentType")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            Record data = _mapper.Map<Record>(enrollment);

            return new OkObjectResult(new
            {
                data,
                options = new { _context.Sections }
            });
        }

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/FINANCEAPPROVAL/NEW")]
        public dynamic Create([FromRoute]string enrollmentNumber, [FromBody]Form form)
        {
            Hashtable errors = new Hashtable();
            Enrollment enrollment = _context.Enrollments
                                            .Include("Pendencies")
                                            .Include("Pendencies.Section")
                                            .Include("PersonalData")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            if (ModelState.IsValid)
            {
                foreach (Models.Pendency pendency in enrollment.Pendencies.ToList())
                {
                    if (!form.Pendencies.Any(c => c.Id == pendency.Id))
                    {
                        if (pendency is Models.FinancePendency)
                        {
                            _context.Set<Models.Pendency>().Remove(pendency);
                        }
                    }
                }
                foreach (ViewModels.Pendency pendency in form.Pendencies)
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

                if (form.Pendencies.Count() == 0)
                {
                    enrollment.FinanceApproval = DateTime.Now;
                }

                List<string> messages = new List<string>();

                _context.Set<Enrollment>().Update(enrollment);

                _context.SaveChanges();

                _context.Entry(enrollment).Reload();
            }
            else
            {
                errors = GetErrors();
            }

            return new OkObjectResult(new { errors, data = _mapper.Map<Record>(enrollment) });
        }

        private string FinanceDataState(FinanceData financeData)
        {
            FinanceDataValidator validator = new FinanceDataValidator(_context);
            FluentValidation.Results.ValidationResult results = validator.Validate(financeData);
            FinanceDataMessagesValidator messagesValidator = new FinanceDataMessagesValidator(_context);
            FluentValidation.Results.ValidationResult resultsMessages = messagesValidator.Validate(financeData);

            if (!financeData.UpdatedAt.HasValue)
            {
                return "empty";
            }
            if (results.IsValid && resultsMessages.IsValid)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }
    }
}
