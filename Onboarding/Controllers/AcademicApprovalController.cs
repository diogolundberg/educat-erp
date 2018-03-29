
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onboarding.Models;
using Onboarding.ViewModel;

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
            List<AcademicApprovalViewModel> approvalList = _mapper.Map<List<AcademicApprovalViewModel>>(enrollments);
            return new { records = approvalList };
        }
    }
}
