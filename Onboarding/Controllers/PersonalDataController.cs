using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.ViewModel;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonalDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<PersonalData> _personalDataRepository;

        public PersonalDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _personalDataRepository = new BaseRepository<PersonalData>(_context);
        }

        [HttpPost("{token}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]PersonalDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            PersonalData personalData = _context.Set<PersonalData>()
                                                .Include(x => x.Enrollment)
                                                .AsNoTracking()
                                                .Single(x => x.Enrollment.ExternalId == token);

            if (personalData == null)
            {
                return NotFound();
            }

            if (personalData.Enrollment.SendDate.HasValue || !personalData.Enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            personalData = _mapper.Map<PersonalData>(obj);
            _context.Set<PersonalData>().Update(personalData);
            foreach (PersonalDataDocument document in personalData.PersonalDataDocuments)
            {
                if (document.Document.Id == 0)
                {
                    _context.Set<PersonalDataDocument>().Add(document);
                }
                else
                {
                    _context.Set<PersonalDataDocument>().Update(document);
                }
            }
            _context.SaveChanges();

            PersonalDataViewModel viewModel = _mapper.Map<PersonalDataViewModel>(personalData);
            viewModel.State = PersonalDataState(personalData);

            var errors = ModelState.ToDictionary(
                modelState => modelState.Key,
                modelState => modelState.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return new OkObjectResult(new
            {
                errors,
                data = viewModel
            });
        }

        private string PersonalDataState(PersonalData personalData)
        {
            if (personalData.UpdatedAt.HasValue)
            {
                return "empty";
            }

            if (ModelState.IsValid)
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
