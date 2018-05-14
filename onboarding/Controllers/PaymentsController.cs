using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

namespace onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PaymentsController : BaseController
    {
        private readonly IConfiguration _configuration;

        public PaymentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}", Name = "ONBOARDING/PAYMENTS/GET")]
        public dynamic GetById([FromRoute]int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(_configuration["FINANCE_HOST"] + "/api/invoices/" + id).Result;
                return JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
        }
    }
}