using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using onboarding.Models;
using onboarding.ViewModels.FinanceDatas;
using onboarding.ViewModels.Payments;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace onboarding.Services
{
    public class PaymentService : BaseService<Enrollment>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PaymentService(DatabaseContext context, IConfiguration configuration, IMapper mapper) : base(context)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public Enrollment Create(Enrollment enrollment)
        {
            return GenerateBillet(enrollment);
        }

        private Enrollment GenerateBillet(Enrollment enrollment)
        {
            Invoice invoice = null;

            using (HttpClient client = new HttpClient())
            {
                RepresentativeViewModel representative = null;

                if (enrollment.FinanceData.Representative is RepresentativeCompany)
                {
                    representative = _mapper.Map<RepresentativeCompanyViewModel>(enrollment.FinanceData.Representative);
                }
                else
                {
                    _context.Entry((RepresentativePerson)enrollment.FinanceData.Representative).Reference(x => x.Relationship).Load();
                    representative = _mapper.Map<RepresentativePersonViewModel>(enrollment.FinanceData.Representative);
                }

                representative.Discriminator = enrollment.FinanceData.Representative.GetType().Name;

                invoice = new Invoice
                {
                    InvoiceNumber = "",
                    Value = enrollment.FinanceData.Plan.Value,
                    DueDate = enrollment.FinanceData.Plan.DueDate,
                    Items = new List<Item>() { new Item { EnrollmentNumber = enrollment.ExternalId } },
                    Representative = representative
                };

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(invoice), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = client.PostAsync(_configuration["FINANCE_HOST"] + "/api/invoices/", stringContent).Result;

                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                invoice = JsonConvert.DeserializeObject<Invoice>(JsonConvert.SerializeObject(((dynamic)JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result)).data));

                enrollment.Payment = enrollment.Payment == null ? new Payment() : enrollment.Payment;
                enrollment.Payment.BilletUrl = invoice.Billet;
                enrollment.Payment.InvoiceNumber = invoice.Id;

                _context.Enrollments.Update(enrollment);
                _context.SaveChanges();
            }

            return enrollment;
        }
    }
}
