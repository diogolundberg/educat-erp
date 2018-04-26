using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using onboarding.Models;

namespace onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SchedulingController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public SchedulingController(DatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        //[HttpGet("", Name = "ONBOARDING/SCHEDULING/LIST")]
        //public dynamic GetList()
        //{
        //}

        //[HttpGet("{id}", Name = "ONBOARDING/SCHEDULING/GET")]
        //public IActionResult GetById([FromRoute]int id)
        //{
        //}

        //[HttpPost(Name = "ONBOARDING/SCHEDULING/NEW")]
        //public dynamic Create([FromBody]Form form)
        //{
        //}

        //[HttpPut("{id}", Name = "ONBOARDING/SCHEDULING/EDIT")]
        //public dynamic Edit([FromRoute]int id, [FromBody]Form form)
        //{
        //}
    }
}