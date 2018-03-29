using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Onboarding.Data.Entity;
using Onboarding.Models;
using Onboarding.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FinanceDataController : Controller
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public FinanceDataController(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = databaseContext;
        }

        [HttpPost("{token}", Name = "ONBOARDING/FINANCEDATA/EDIT")]
        public IActionResult Update([FromRoute]string token, [FromBody]FinanceDataViewModel obj)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            FinanceData financeData = _context.Set<FinanceData>()
                                              .Include("Enrollment")
                                              .Include("Representative")
                                              .Include("Guarantors")
                                              .SingleOrDefault(x => x.Id == obj.Id);

            if (financeData == null)
            {
                return NotFound();
            }

            if (financeData.Enrollment.SendDate.HasValue || !financeData.Enrollment.IsDeadlineValid())
            {
                return BadRequest();
            }

            FinanceData financeDataMapper = _mapper.Map<FinanceData>(obj);
            _context.Entry(financeData).CurrentValues.SetValues(financeDataMapper);

            _context.SaveChanges();
            _context.Entry(financeData).Reload();

            FinanceDataViewModel viewModel = _mapper.Map<FinanceDataViewModel>(financeData);
            viewModel.State = FinanceDataState(viewModel);

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

        private string FinanceDataState(FinanceDataViewModel financeData)
        {
            System.ComponentModel.DataAnnotations.ValidationContext context = new System.ComponentModel.DataAnnotations.ValidationContext(financeData);
            List<ValidationResult> result = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(financeData, context, result, true);

            if (!financeData.UpdatedAt.HasValue)
            {
                return "empty";
            }

            if (valid)
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
