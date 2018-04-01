
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;
using Onboarding.ViewModels;

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
            List<Enrollment> enrollments = _context.Enrollments.Include("PersonalData").ToList();
            List<FinanceApprovalViewModel> approvalList = _mapper.Map<List<FinanceApprovalViewModel>>(enrollments);
            return new { records = approvalList };
        }
    }
}
