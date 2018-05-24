using AutoMapper;
using onboarding.Models;
using onboarding.Statuses;
using onboarding.Validations;
using onboarding.Validations.FinanceData;
using onboarding.Validations.PersonalData;
using onboarding.ViewModels.Enrollments;
using System.Collections.Generic;

namespace onboarding.Resolvers
{
    public class CardResolver : IValueResolver<Enrollment, Record, IEnumerable<Card>>
    {
        private readonly DatabaseContext _context;

        public CardResolver(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Card> Resolve(Enrollment source, Record destination, IEnumerable<Card> destMember, ResolutionContext context)
        {
            return new List<Card> {
                new Card
                    {
                        Name = "Dados pessoais",
                        ResourceId = "PersonalDatas",
                        Status = (new PersonalDataStatus(new PersonalDataValidator(_context), source.PersonalData)).GetStatus(),
                    },
                    new Card
                    {
                        Name = "Informações da Matrícula",
                        ResourceId = "CourseSummaries",
                        Status = (new CourseSummaryStatus(null, source)).GetStatus()
                    },
                    new Card
                    {
                        Name = "Dados financeiros",
                        ResourceId = "FinanceDatas",
                        Status = (new FinanceDataStatus(new FinanceDataValidator(_context), source.FinanceData, new FinanceDataMessagesValidator(_context))).GetStatus()
                    },
                    new Card
                    {
                        Name = "Enviar para aprovação",
                        ResourceId = "EnrollmentSummaries",
                        Status = (new EnrollmentSummaryStatus(null, source)).GetStatus()
                    },
                    new Card
                    {
                        Name = "Contrato",
                        ResourceId = "Contracts",
                        Status = (new ContractStatus(null, source)).GetStatus()
                    },
                    new Card
                    {
                        Name = "Pagamento",
                        ResourceId = "Payments",
                        Status = (new PaymentStatus(null, source.Invoice)).GetStatus()
                    },
            };
        }
    }
}
