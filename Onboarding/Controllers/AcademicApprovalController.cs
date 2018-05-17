using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;
using onboarding.Statuses;
using onboarding.ViewModels.AcademicApprovals;

namespace onboarding.Controllers
{
    public class AcademicApprovalController : BaseController<Enrollment>
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
                                                   .Include("Pendencies")
                                                   .Include("Pendencies.Section")
                                                   .Include("PersonalData")
                                                   .Include("PersonalData.PersonalDataDocuments")
                                                   .Include("PersonalData.PersonalDataSpecialNeeds")
                                                   .Include("PersonalData.PersonalDataDocuments.Document")
                                                   .Where(x => x.SentAt.HasValue && !x.AcademicApproval.HasValue && x.AcademicPendencies.Count() == 0)
                                                   .ToList();

            List<Records> records = _mapper.Map<List<Records>>(enrollments);

            return new { records };
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/ACADEMICAPPROVAL/GET")]
        public IActionResult GetById([FromRoute]string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Pendencies")
                                            .Include("Pendencies.Section")
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
                                            .Include("PersonalData.HighSchoolKind")
                                            .Include("PersonalData.Nationality")
                                            .Include("PersonalData.PersonalDataDisabilities.Disability")
                                            .Include("PersonalData.PersonalDataSpecialNeeds.SpecialNeed")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("PersonalData.PersonalDataDocuments.Document.DocumentType")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentNumberIsNotValid } });
            }

            Record data = _mapper.Map<Record>(enrollment.PersonalData);
            data.Status = (new AcademicApprovalStatus(null, enrollment)).GetStatusName();

            return new OkObjectResult(new
            {
                data,
                options = new { Sections = _context.Sections.Where(x => x is AcademicSection).ToList() }
            });
        }

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/ACADEMICAPPROVAL/NEW")]
        public dynamic Create([FromRoute]string enrollmentNumber, [FromBody]Form form)
        {
            Hashtable errors = new Hashtable();
            Enrollment enrollment = _context.Enrollments
                                            .Include("Pendencies")
                                            .Include("Pendencies.Section")
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
                                            .Include("PersonalData.HighSchoolKind")
                                            .Include("PersonalData.Nationality")
                                            .Include("PersonalData.PersonalDataDisabilities.Disability")
                                            .Include("PersonalData.PersonalDataSpecialNeeds.SpecialNeed")
                                            .Include("PersonalData.PersonalDataDocuments.Document")
                                            .Include("PersonalData.PersonalDataDocuments.Document.DocumentType")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentNumberIsNotValid } });
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
    }
}
