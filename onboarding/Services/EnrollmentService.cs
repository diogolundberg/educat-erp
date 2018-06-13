using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using onboarding.Models;
using onboarding.ViewModels.FinanceDatas;
using onboarding.ViewModels.Payments;

namespace onboarding.Services
{
    public class EnrollmentService : BaseService<Enrollment>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EnrollmentService(DatabaseContext context, IConfiguration configuration, IMapper mapper) : base(context)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public override IQueryable<Enrollment> List()
        {
            return base.List()
                        .Include("Onboarding")
                        .Include("Contract")
                        .Include("EnrollmentSteps")
                        .Include("EnrollmentSteps.Pendencies")
                        .Include("EnrollmentSteps.Pendencies.Section")
                        .Include("PersonalData")
                        .Include("PersonalData.PersonalDataDisabilities")
                        .Include("PersonalData.PersonalDataSpecialNeeds")
                        .Include("PersonalData.PersonalDataDocuments")
                        .Include("PersonalData.PersonalDataDocuments.Document")
                        .Include("PersonalData.BirthCountry")
                        .Include("FinanceData")
                        .Include("FinanceData.Plan")
                        .Include("FinanceData.Representative")
                        .Include("FinanceData.Representative.AddressKind")
                        .Include("FinanceData.Representative.City")
                        .Include("FinanceData.Representative.State")
                        .Include("FinanceData.Guarantors")
                        .Include("FinanceData.Guarantors.Relationship")
                        .Include("FinanceData.Guarantors.GuarantorDocuments")
                        .Include("FinanceData.Guarantors.GuarantorDocuments.Document");
        }

        public Invoice GetInvoice(Enrollment enrollment)
        {
            if (!enrollment.InvoiceId.HasValue)
            {
                return GenerateBillet(enrollment);
            }

            return GetInvoice(enrollment.InvoiceId);
        }

        private Invoice GenerateBillet(Enrollment enrollment)
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

                enrollment.InvoiceId = invoice.Id;
                _context.Enrollments.Update(enrollment);
                _context.SaveChanges();
            }

            return invoice;
        }

        private Invoice GetInvoice(int? invoiceId)
        {
            Invoice invoice = null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = client.GetAsync(_configuration["FINANCE_HOST"] + "/api/invoices/" + invoiceId).Result;
                invoice = JsonConvert.DeserializeObject<Invoice>(JsonConvert.SerializeObject(((dynamic)JsonConvert.DeserializeObject(httpResponseMessage.Content.ReadAsStringAsync().Result)).data));
            }

            return invoice;
        }
    }
}
