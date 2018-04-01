
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
            List<Enrollment> enrollments = _context.Enrollments.Include("FinanceData").Where(x => x.SentAt.HasValue).ToList();
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
                                            .Include("EnrollmentPendencies")
                                            .Include("EnrollmentPendencies.Pendency")
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

            foreach (Models.EnrollmentPendency enrollmentPendency in enrollment.EnrollmentPendencies.ToList())
            {
                if (!form.FinancePendencies.Any(c => c.Id == enrollmentPendency.PendencyId))
                {
                    if (enrollmentPendency.Pendency is Models.FinancePendency)
                    {
                        _context.Set<Models.EnrollmentPendency>().Remove(enrollmentPendency);
                        _context.Set<Models.Pendency>().Remove(enrollmentPendency.Pendency);
                    }
                }
            }
            foreach (ViewModels.Pendency pendency in form.FinancePendencies)
            {
                Models.EnrollmentPendency existingEnrolmentPendency = enrollment.EnrollmentPendencies.Where(c => c.Pendency.Id == pendency.Id).SingleOrDefault();

                if (existingEnrolmentPendency == null)
                {
                    Models.EnrollmentPendency enrollmentPendency = new EnrollmentPendency
                    {
                        Pendency = new Models.FinancePendency
                        {
                            SectionId = pendency.SectionId.Value,
                            Description = pendency.Description,
                        },
                        EnrollmentId = enrollment.Id
                    };

                    _context.Set<EnrollmentPendency>().Add(enrollmentPendency);
                }
                else
                {
                    existingEnrolmentPendency.Pendency.Description = pendency.Description;
                    existingEnrolmentPendency.Pendency.SectionId = pendency.SectionId.Value;

                    _context.Set<Models.Pendency>().Update(existingEnrolmentPendency.Pendency);
                }
            }

            List<string> messages = new List<string>();

            _context.SaveChanges();

            messages.Add("Pendências cadastradas com sucesso.");

            // TODO: Persistir as pendencias
            // TODO: Adicionar lógica da maquina de estado
            //   -> Quando não houver pendências, aprovar (preencher AcademicApproval)
            //   -> (academicAppoval || academicPendencies) && (financeAppoval || financePendencies) = preenche reviewed at
            //   -> (reviewed_at && (academicPendencies || financePendencies)) = sent at zerado

            return new OkObjectResult(new { messages });
        }
    }
}
