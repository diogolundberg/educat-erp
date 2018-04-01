
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;
using Onboarding.ViewModels.AcademicApprovals;

namespace Onboarding.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AcademicApprovalController : Controller
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
            List<Enrollment> enrollments = _context.Enrollments.Include("PersonalData").ToList();
            List<Records> records = _mapper.Map<List<Records>>(enrollments);
            return new { records };
        }

        [HttpGet("{enrollmentNumber}", Name = "ONBOARDING/ACADEMICAPPROVAL/GET")]
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

        [HttpPost("", Name = "ONBOARDING/ACADEMICAPPROVAL/NEW")]
        public dynamic Create([FromBody]Form form)
        {
            Enrollment enrollment = _context.Enrollments.SingleOrDefault(x => x.ExternalId == form.EnrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            // TODO: Persistir as pendencias
            // TODO: Adicionar lógica da maquina de estado
            //   -> Quando não houver pendências, aprovar (preencher AcademicApproval)
            //   -> (academicAppoval || academicPendencies) && (financeAppoval || financePendencies) = preenche reviewed at
            //   -> (reviewed_at && (academicPendencies || financePendencies)) = sent at zerado

            return new OkObjectResult(new { });
        }
    }
}
