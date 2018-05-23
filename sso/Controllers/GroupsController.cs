using System;
using SSO.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using SSO.Data.Entity;

namespace SSO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        private readonly DatabaseContext _context;

        private BaseRepository<Group> _groupRepository;

        public GroupsController(DatabaseContext context)
        {
            _context = context;
            _groupRepository = new BaseRepository<Group>(context);
        }

        [HttpGet("", Name = "SSO/GROUPS/LIST")]
        public IEnumerable<Group> GetList()
        {
            return _groupRepository.List();
        }

        [HttpGet("{id}", Name = "SSO/GROUPS/GET")]
        public IActionResult GetById([FromQuery]int id)
        {
            Group item = _groupRepository.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost("", Name = "SSO/GROUPS/NEW")]
        public IActionResult Create([FromBody]Group obj)
        {
            _groupRepository.Add(obj);

            return new ObjectResult(obj);
        }

        [HttpPut("{id}", Name = "SSO/GROUPS/EDIT")]
        public IActionResult Update(int id, [FromBody]Group obj)
        {
            if (obj == null || obj.ExternalId != id)
            {
                return BadRequest();
            }

            Group objOld = _groupRepository.GetById(id);

            if (objOld == null)
            {
                return NotFound();
            }

            Group objNew = (Group)objOld.Clone();
            objNew.Name = obj.Name;
            objNew.Permissions = obj.Permissions;

            _groupRepository.Update(objOld, objNew);

            return new OkResult();
        }

        [HttpDelete("{id}", Name = "SSO/GROUPS/DELETE")]
        public IActionResult Delete(int id)
        {
            Group obj = _groupRepository.GetById(id);

            if (obj == null)
            {
                return NotFound();
            }

            _groupRepository.Delete(obj);

            return new OkResult();
        }
    }
}
