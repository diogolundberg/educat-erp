using Microsoft.AspNetCore.Mvc;
using Onboarding.Models;
using Onboarding.ViewModels.Avatar;
using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/Avatar")]
    public class AvatarController : BaseController
    {
        private readonly DatabaseContext _context;

        public AvatarController(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        [HttpPost("{token}", Name = "ONBOARDING/AVATAR")]
        public dynamic Post([FromRoute]string token,[FromBody]Form form)
        {
            Enrollment enrollment = _context.Enrollments.Single(x => x.ExternalId == token);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "Link para matrícula inválido." } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { "O prazo para esta matrícula foi encerrado." } });
            }

            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new { errors = GetErrors() });
            }

            enrollment.Photo = form.photo;
            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();

            return new OkObjectResult(new { messages = new List<string> { "Foto atualizada com sucesso." } });
        }
    }
}