using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EnrollmentInfosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public EnrollmentInfosController(DatabaseContext databaseContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = databaseContext;
            _mapper = mapper;
        }

        [HttpPost("{token}", Name = "ONBOARDING/ENROLLMENTINFOS/POST")]
        public dynamic Post(string token)
        {
            Enrollment enrollment = _context.Enrollments.Include("Onboarding").Single(x => x.ExternalId == token);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Link para matrícula inválido" } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "O prazo para esta matrícula foi encerrado" } });
            }

            enrollment.EnrollmentInfo = DateTime.Now;

            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();

            return Ok();
        }
    }
}