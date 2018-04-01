
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
    [Authorize]
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
            List<Enrollment> enrollments = _context.Enrollments.Include("FinanceData").ToList();
            List<Records> approvalList = _mapper.Map<List<Records>>(enrollments);
            return new { records = approvalList };
        }

        [HttpPost("", Name = "ONBOARDING/FINANCEAPPROVAL/NEW")]
        public dynamic Create(Form form)
        {
            Enrollment enrollment = _context.Enrollments.SingleOrDefault(x => x.ExternalId == form.EnrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Número de matrícula inválido." } });
            }

            return new OkObjectResult(new { });
        }
    }
}
