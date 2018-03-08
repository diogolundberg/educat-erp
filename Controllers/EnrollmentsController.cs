using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Data.Entity;
using Onboarding.Models;

namespace Onboarding.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EnrollmentsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly BaseRepository<AddressType> _addressTypeRepository;
        private readonly BaseRepository<CivilStatus> _civilStatusRepository;
        private readonly BaseRepository<Country> _countryRepository;
        private readonly BaseRepository<Gender> _genderRepository;
        private readonly BaseRepository<Nationality> _nationalityRepository;
        private readonly BaseRepository<PhoneType> _phoneTypeRepository;
        private readonly BaseRepository<Race> _RaceRepository;
        private readonly BaseRepository<School> _schoolRepository;

        public EnrollmentsController(DatabaseContext databaseContext)
        {
            _context = databaseContext;
            _addressTypeRepository = new BaseRepository<AddressType>(_context);
            _civilStatusRepository = new BaseRepository<CivilStatus>(_context);
            _countryRepository = new BaseRepository<Country>(_context);
            _genderRepository = new BaseRepository<Gender>(_context);
            _nationalityRepository = new BaseRepository<Nationality>(_context);
            _phoneTypeRepository = new BaseRepository<PhoneType>(_context);
            _RaceRepository = new BaseRepository<Race>(_context);
            _schoolRepository = new BaseRepository<School>(_context);
        }

        [HttpGet("")]
        public dynamic Get ()
        {
            return new { 
                AddressTypes = _addressTypeRepository.List(),
                CivilStatus = _civilStatusRepository.List(),
                Countries = _countryRepository.List(),
                Genders = _genderRepository.List(),
                Nationalities = _nationalityRepository.List(),
                PhoneType = _phoneTypeRepository.List(),
                Race = _RaceRepository.List(),
                School = _schoolRepository.List(),
                States = new List<State>() { new State { Name = "MG" } }
            };
        }
    }
}