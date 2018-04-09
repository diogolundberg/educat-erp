
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;
using Onboarding.Validations;
using Onboarding.Validations.PersonalData;
using Onboarding.ViewModels.AcademicApprovals;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AcademicApprovalController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public AcademicApprovalController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpGet("", Name = "ONBOARDING/ACADEMICAPPROVAL/LIST")]
        public dynamic GetList()
        {
            List<Enrollment> enrollments = _context.Enrollments
                                                   .Include("Onboarding")
                                                   .Include("PersonalData")
                                                   .Include("PersonalData.PersonalDataDocuments")
                                                   .Include("PersonalData.PersonalDataSpecialNeeds")
                                                   .Include("PersonalData.PersonalDataDocuments.Document")
                                                   .Where(x => x.SentAt.HasValue).ToList();

            List<Records> records = _mapper.Map<List<Records>>(enrollments);

            foreach (Records record in records)
            {
                PersonalData personalData = enrollments.Single(x => x.ExternalId == record.EnrollmentNumber).PersonalData;
                record.State = PersonalDataState(personalData);
            }

            return new { records };
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/ACADEMICAPPROVAL/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Pendencies")
                                            .Include("PersonalData.Gender")
                                            .Include("PersonalData.MaritalStatus")
                                            .Include("PersonalData.BirthCity")
                                            .Include("PersonalData.BirthState")
                                            .Include("PersonalData.BirthCountry")
                                            .Include("PersonalData.HighSchoolGraduationCountry")
                                            .Include("PersonalData.City")
                                            .Include("PersonalData.State")
                                            .Include("PersonalData.AddressKind")
                                            .Include("PersonalData.Race")
                                            .Include("PersonalData.HighSchollKind")
                                            .Include("PersonalData.Nationality")
                                            .Include("PersonalData.PersonalDataDisabilities.Disability")
                                            .Include("PersonalData.PersonalDataSpecialNeeds.SpecialNeed")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("PersonalData.PersonalDataDocuments.Document.DocumentType")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            Record data = _mapper.Map<Record>(enrollment.PersonalData);

            return new OkObjectResult(new
            {
                data,
                options = new { _context.Sections }
            });
        }

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/ACADEMICAPPROVAL/NEW")]
        public dynamic Create([FromRoute]string enrollmentNumber, [FromBody]Form form)
        {
            Hashtable errors = new Hashtable();
            Enrollment enrollment = _context.Enrollments
                                            .Include("Pendencies")
                                            .Include("PersonalData.Gender")
                                            .Include("PersonalData.MaritalStatus")
                                            .Include("PersonalData.BirthCity")
                                            .Include("PersonalData.BirthState")
                                            .Include("PersonalData.BirthCountry")
                                            .Include("PersonalData.HighSchoolGraduationCountry")
                                            .Include("PersonalData.City")
                                            .Include("PersonalData.State")
                                            .Include("PersonalData.AddressKind")
                                            .Include("PersonalData.Race")
                                            .Include("PersonalData.HighSchollKind")
                                            .Include("PersonalData.Nationality")
                                            .Include("PersonalData.PersonalDataDisabilities.Disability")
                                            .Include("PersonalData.PersonalDataSpecialNeeds.SpecialNeed")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("PersonalData.PersonalDataDocuments.Document.DocumentType")
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
                        if (pendency is Models.AcademicPendency)
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
                        _context.Set<Models.Pendency>().Add(new Models.AcademicPendency
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
                    enrollment.AcademicApproval = DateTime.Now;
                }

                if ((enrollment.AcademicApproval.HasValue || form.Pendencies.Count() > 0)
                    && (enrollment.FinanceApproval.HasValue || enrollment.Pendencies.OfType<Models.FinancePendency>().Count() > 0))
                {
                    enrollment.ReviewedAt = DateTime.Now;
                }

                if (enrollment.ReviewedAt.HasValue && (form.Pendencies.Count() > 0 || enrollment.Pendencies.OfType<Models.FinancePendency>().Count() > 0))
                {
                    enrollment.SentAt = null;
                }

                _context.Set<Enrollment>().Update(enrollment);

                _context.SaveChanges();

                _context.Entry(enrollment).Reload();
            }
            else
            {
                errors = GetErrors();
            }

            return new OkObjectResult(new { errors, data = _mapper.Map<Record>(enrollment.PersonalData) });
        }

        private string PersonalDataState(PersonalData personalData)
        {
            PersonalDataValidator validator = new PersonalDataValidator(_context);
            FluentValidation.Results.ValidationResult results = validator.Validate(personalData);

            if (!personalData.UpdatedAt.HasValue)
            {
                return "empty";
            }
            if (results.IsValid)
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
