﻿using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly BaseRepository<Enrollment> _enrollmentRepository;
        private readonly BaseRepository<PersonalData> _personalDataRepository;

        public PersonalDataController(DatabaseContext databaseContext, IConfiguration configuration, IMapper mapper)
        {
            _context = databaseContext;
            _configuration = configuration;
            _mapper = mapper;
            _enrollmentRepository = new BaseRepository<Enrollment>(_context);
            _personalDataRepository = new BaseRepository<PersonalData>(_context);
        }

        [HttpPatch("{token}", Name = "ONBOARDING/PERSONALDATA/PATCHEDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]PersonalDataPatchViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
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

            PersonalData oldPersonalData = enrollment.PersonalData;
            PersonalData newPersonalData = _mapper.Map<PersonalData>(obj);

            newPersonalData.EnrollmentId = oldPersonalData.EnrollmentId;
            newPersonalData.RealName = oldPersonalData.RealName;
            newPersonalData.CPF = oldPersonalData.CPF;
            newPersonalData.Email = oldPersonalData.Email;

            _personalDataRepository.Update(oldPersonalData, newPersonalData);

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

        [HttpPost("{token}", Name = "ONBOARDING/PERSONALDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]PersonalDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
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

            PersonalData oldPersonalData = enrollment.PersonalData;

            Enrollment newEnrollment = (Enrollment)enrollment.Clone();

            _enrollmentRepository.Update(enrollment, newEnrollment);

            _context.Entry(newEnrollment).Reference(x => x.PersonalData).Load();

            PersonalData newPersonalData = _mapper.Map<PersonalData>(obj);

            newPersonalData.ExternalId = newEnrollment.PersonalData.ExternalId;
            newPersonalData.EnrollmentId = newEnrollment.Id;
            newPersonalData.RealName = oldPersonalData.RealName;
            newPersonalData.CPF = oldPersonalData.CPF;
            newPersonalData.Email = oldPersonalData.Email;

            _personalDataRepository.Update(oldPersonalData, newPersonalData);

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
