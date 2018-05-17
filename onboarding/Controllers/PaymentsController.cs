using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using onboarding.Models;
using onboarding.ViewModels;
using onboarding.ViewModels.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace onboarding.Controllers
{
    public class PaymentsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public PaymentsController(IConfiguration configuration, IMapper mapper, DatabaseContext context)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
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

        [HttpPut("{enrollmentNumber}", Name = "ONBOARDING/PAYMENTS/EDIT")]
        public dynamic Edit(string enrollmentNumber)
        {
            Enrollment enrollment = _context.Enrollments
                                            .Include("Onboarding")
                                            .Include("FinanceData.Plan")
                                            .Include("FinanceData.Representative")
                                            .SingleOrDefault(x => x.ExternalId == enrollmentNumber);

            if (enrollment == null)
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.EnrollmentLinkIsNotValid } });
            }

            if (!enrollment.IsDeadlineValid())
            {
                return new BadRequestObjectResult(new { messages = new List<string> { onboarding.Resources.Messages.OnboardingExpired } });
            }

            using (HttpClient client = new HttpClient())
            {
                RepresentativeViewModel representative = enrollment.FinanceData.Representative is RepresentativeCompany ?
                                                        (RepresentativeViewModel)_mapper.Map<RepresentativeCompanyViewModel>(enrollment.FinanceData.Representative)
                                                        : (RepresentativeViewModel)_mapper.Map<RepresentativePersonViewModel>(enrollment.FinanceData.Representative);
                representative.Discriminator = enrollment.FinanceData.Representative.GetType().Name;

                Invoice invoice = new Invoice
                {
                    InvoiceNumber = "",
                    Value = enrollment.FinanceData.Plan.Value,
                    DueDate = "25/12/2018",
                    Items = new List<Item>() { new Item { EnrollmentNumber = enrollmentNumber } },
                    Representative = representative
                };

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(invoice), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = client.PostAsync(_configuration["FINANCE_HOST"] + "/api/invoices/", stringContent).Result;

                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result);
                }

                dynamic resultObj = JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result);

                enrollment.InvoiceId = resultObj.data.id;
                _context.Enrollments.Update(enrollment);
                _context.SaveChanges();

                return resultObj;
            }
        }
    }
}