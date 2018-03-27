using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FinanceDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<FinanceData> _financeDataRepository;

        public FinanceDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _configuration = configuration;
            _mapper = mapper;
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _financeDataRepository = new BaseRepository<FinanceData>(_context);
        }

        [HttpPatch("{token}", Name = "ONBOARDING/FINANCEDATA/PATCHEDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]FinanceDataPatchViewModel obj)
        {
            if (string.IsNullOrEmpty(token) || obj == null)
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetByExternalId(token);
            _context.Entry(enrollment).Reference(x => x.PersonalData).Load();

            if (enrollment == null)
            {
                return NotFound();
            }

            if (enrollment.SendDate.HasValue || !enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            FinanceData oldFinanceData = enrollment.FinanceData;

            Enrollment newEnrollment = (Enrollment)enrollment.Clone();

            _enrollmentRepository.Update(enrollment, newEnrollment);

            _context.Entry(newEnrollment).Reference(x => x.FinanceData).Load();

            FinanceData newFinanceData = _mapper.Map<FinanceData>(obj);

            newFinanceData.EnrollmentId = newEnrollment.Id;
            
            if (oldFinanceData == null)
            {
                _financeDataRepository.Add(newFinanceData);
            }
            else
            {
                newFinanceData.ExternalId = newEnrollment.FinanceData.ExternalId;
                _financeDataRepository.Update(oldFinanceData, newFinanceData);
            }

            var errors = ModelState.ToDictionary(
                modelState => modelState.Key,
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return new OkObjectResult(new
            {
                errors,
                data = obj
            });
        }

        [HttpPost("{token}", Name = "ONBOARDING/FINANCEDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]FinanceDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token) || obj == null)
            {
                return BadRequest();
            }

            Enrollment enrollment = _enrollmentRepository.GetByExternalId(token);
            _context.Entry(enrollment).Reference(x => x.PersonalData).Load();

            if (enrollment == null)
            {
                return NotFound();
            }

            if (enrollment.SendDate.HasValue || !enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            FinanceData oldFinanceData = enrollment.FinanceData;

            Enrollment newEnrollment = (Enrollment)enrollment.Clone();

            _enrollmentRepository.Update(enrollment, newEnrollment);

            _context.Entry(newEnrollment).Reference(x => x.FinanceData).Load();

            FinanceData newFinanceData = _mapper.Map<FinanceData>(obj);

            newFinanceData.EnrollmentId = newEnrollment.Id;

            if (oldFinanceData == null)
            {
                _financeDataRepository.Add(newFinanceData);
            }
            else
            {
                newFinanceData.ExternalId = newEnrollment.FinanceData.ExternalId;
                _financeDataRepository.Update(oldFinanceData, newFinanceData);
            }

            var errors = ModelState.ToDictionary(
                modelState => modelState.Key,
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return new OkObjectResult(new
            {
                errors,
                data = obj
            });
        }
    }
}
