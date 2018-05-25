using System.Linq;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;

namespace onboarding.Services
{
    public class PersonalDataService : BaseService<PersonalData>
    {
        public PersonalDataService(DatabaseContext context) : base(context)
        {
        }

        public override IQueryable<PersonalData> List()
        {
            return base.List()
                        .Include("Nationality")
                        .Include("Gender")
                        .Include("HighSchoolGraduationCountry")
                        .Include("Enrollment.Onboarding")
                        .Include("Enrollment")
                        .Include("Enrollment.Pendencies")
                        .Include("Enrollment.FinanceData")
                        .Include("Enrollment.FinanceData.Guarantors")
                        .Include("Enrollment.FinanceData.Representative")
                        .Include("PersonalDataDocuments")
                        .Include("PersonalDataSpecialNeeds")
                        .Include("PersonalDataDisabilities")
                        .Include("PersonalDataDocuments.Document");
        }

        public PersonalData Update(PersonalData obj, PersonalData objToUpdate)
        {
            _context.Entry(obj).CurrentValues.SetValues(objToUpdate);

            foreach (PersonalDataDocument personalDataDocument in obj.PersonalDataDocuments.ToList())
            {
                if (!objToUpdate
                    .PersonalDataDocuments
                    .Any(c => c.Document.Id == personalDataDocument.DocumentId))
                {
                    _context.Set<PersonalDataDocument>().Remove(personalDataDocument);
                    _context.Set<Document>().Remove(_context.Set<Document>().Find(personalDataDocument.DocumentId));
                }
            }
            foreach (PersonalDataDocument personalDataDocument in objToUpdate.PersonalDataDocuments)
            {
                PersonalDataDocument existingPersonalDataDocument = obj.PersonalDataDocuments
                    .Where(c => c.DocumentId == personalDataDocument.Document.Id)
                    .SingleOrDefault();

                if (existingPersonalDataDocument != null)
                {
                    personalDataDocument.Document.Id = existingPersonalDataDocument.Document.Id;
                    _context.Entry(existingPersonalDataDocument.Document).CurrentValues.SetValues(personalDataDocument.Document);
                }
                else
                {
                    personalDataDocument.PersonalDataId = obj.Id;
                    personalDataDocument.Document.Id = 0;
                    _context.Set<PersonalDataDocument>().Add(personalDataDocument);
                }
            }

            foreach (PersonalDataDisability personalDataDisibility in obj.PersonalDataDisabilities.ToList())
            {
                if (!objToUpdate
                    .PersonalDataDisabilities
                    .Any(c => c.DisabilityId == personalDataDisibility.DisabilityId))
                {
                    _context.Set<PersonalDataDisability>().Remove(personalDataDisibility);
                }
            }
            foreach (PersonalDataDisability personalDataDisability in objToUpdate.PersonalDataDisabilities)
            {
                PersonalDataDisability existingPersonalDataDisability = obj.PersonalDataDisabilities
                    .Where(c => c.DisabilityId == personalDataDisability.DisabilityId)
                    .SingleOrDefault();

                if (existingPersonalDataDisability == null)
                {
                    obj.PersonalDataDisabilities.Add(personalDataDisability);
                }
            }

            foreach (PersonalDataSpecialNeed personalDataSpecialNeed in obj.PersonalDataSpecialNeeds.ToList())
            {
                if (!objToUpdate
                    .PersonalDataSpecialNeeds
                    .Any(c => c.SpecialNeedId == personalDataSpecialNeed.SpecialNeedId))
                {
                    _context.Set<PersonalDataSpecialNeed>().Remove(personalDataSpecialNeed);
                }
            }
            foreach (PersonalDataSpecialNeed personalDataSpecialNeed in objToUpdate.PersonalDataSpecialNeeds)
            {
                PersonalDataSpecialNeed existingPersonalDataSpecialNeed = obj.PersonalDataSpecialNeeds
                    .Where(c => c.SpecialNeedId == personalDataSpecialNeed.SpecialNeedId)
                    .SingleOrDefault();

                if (existingPersonalDataSpecialNeed == null)
                {
                    obj.PersonalDataSpecialNeeds.Add(personalDataSpecialNeed);
                }
            }

            _context.Entry(obj).Property(x => x.CPF).IsModified = false;
            _context.Entry(obj).Property(x => x.RealName).IsModified = false;
            _context.Entry(obj).Property(x => x.Email).IsModified = false;

            _context.SaveChanges();

            return Find(x => x.Id == obj.Id).FirstOrDefault();
        }
    }
}
