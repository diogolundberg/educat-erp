using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Onboarding.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Onboarding.Controllers
{
    [Produces("application/json")]
    [Route("api/Cep")]
    public class CepController : Controller
    {
        private readonly DatabaseContext _context;

        public CepController(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        [HttpGet("", Name = "ONBOARDING/CEP/GET")]
        public dynamic Get(string cep)
        {
            ViewModels.Cep.Record record = new ViewModels.Cep.Record();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("http://api.postmon.com.br/v1/cep/" + cep).Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic dynamicObj = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

                    string codigoIbgeCity = dynamicObj["cidade_info"]["codigo_ibge"];
                    City city = _context.Cities.FirstOrDefault(x => x.ExternalId == codigoIbgeCity);
                    int? cityId = city != null ? (int?)city.Id : null;

                    string stateName = dynamicObj["estado"];
                    State state = _context.States.FirstOrDefault(x => x.ExternalId == stateName);
                    int? stateId = state != null ? (int?)state.Id : null;

                    record.Cep = dynamicObj["cep"];
                    record.State = dynamicObj["estado"];
                    record.StateId = 0;
                    record.City = dynamicObj["cidade"];
                    record.CityId = cityId;
                    record.StateId = stateId;
                    record.StreetAddress = dynamicObj["logradouro"];
                    record.Neighborhood = dynamicObj["bairro"];
                }
                else
                {
                    record = null;
                }

            }

            return new { record };
        }
    }
}