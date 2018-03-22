using System;
using SSO.Models;
using System.Linq;
using SSO.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SSO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly BaseRepository<User> _userRepository;

        public UsersController(DatabaseContext context)
        {
            _context = context;
            _userRepository = new BaseRepository<User>(_context);
        }

        [HttpGet("", Name = "SSO/USERS/LIST")]
        public IEnumerable<User> GetList()
        {
            return _userRepository.List();
        }

        [HttpGet("{id}", Name = "SSO/USERS/GET")]
        public IActionResult GetById(int id)
        {
            User item = _userRepository.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost("", Name = "SSO/USERS/NEW")]
        public IActionResult Create([FromBody]User obj)
        {
            _userRepository.Add(obj);
            return new ObjectResult(obj);
        }

        [HttpPut("{id}", Name = "SSO/USERS/EDIT")]
        public IActionResult Update(int id, [FromBody]User obj)
        {
            if (obj == null || obj.ExternalId != id)
            {
                return BadRequest();
            }

            User user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            User newUser = (User)user.Clone();
            newUser.Email = obj.Email;
            newUser.Password = obj.Password;

            _userRepository.Update(user, newUser);

            return new OkResult();
        }

        [HttpDelete("{id}", Name = "SSO/USERS/DELETE")]
        public IActionResult Delete(int id)
        {
            User obj = _userRepository.GetById(id);

            if (obj == null)
            {
                return NotFound();
            }

            _userRepository.Delete(obj);

            return new OkResult();
        }
    }
}
