using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public class EnrollmentViewModel 
    {
        public string Name { get; set; }

        public string SocialName { get; set; }

        public string Cpf { get; set; }

        public DateTime? Birthday { get; set; }

        public Guid? CivilStatusId { get; set; }

        public Guid? GenderId { get; set; }

        public Guid? NationalityId { get; set; }

        public Guid? OriginCountryId { get; set; }

        public Guid? BirthStateId { get; set; }

        public string YearofHighSchoolGraduation { get; set; }

        public Guid? CountryOfGraduationFromHighSchoolId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Guid? PhoneTypeId { get; set; }

        public string PhoneNumber { get; set; }

        public string Cep { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public Guid? CountryStateId { get; set; }

        public Guid? AddressTypeId { get; set; }

        public Guid? RaceId { get; set; }

        public Guid? SchoolId { get; set; }

        public string MothersName { get; set; }

        public DateTimeOffset? SendBy { get; set; }

        public bool? HasHandicaps { get; set; }

        public string BirthCity { get; set; }

        public Guid? ResponsibleDocumentTypeId { get; set; }

        public string ResponsibleCpf { get; set; }

        public string ResponsibleCnpj { get; set; }

        public string ResponsibleName { get; set; }

        public string ResponsibleContact { get; set; }

        public string ResponsibleAddress { get; set; }

        public string ResponsiblePhone1 { get; set; }

        public string ResponsiblePhone2 { get; set; }

        public string ResponsibleEmail { get; set; }

        public Guid? GuarantorDocumentTypeId { get; set; }

        public string GuarantorCpf { get; set; }

        public string GuarantorCnpj { get; set; }

        public string GuarantorName { get; set; }

        public string GuarantorContact { get; set; }

        public string GuarantorAddress { get; set; }

        public string GuarantorPhone1 { get; set; }

        public string GuarantorPhone2 { get; set; }

        public string GuarantorEmail { get; set; }

        public IEnumerable<Guid> Deficiencies { get; set; }
    }
}