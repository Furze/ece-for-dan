﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MoE.ECE.Domain.Model.ReferenceData
{
    public class EceService
    {
        public int RefOrganisationId { get; set; }
        public string OrganisationName { get; set; } = null!;
        public string OrganisationNumber { get; set; } = null!;
        public int OrganisationTypeId { get; set; }
        public string? OrganisationTypeDescription { get; set; }
        public int OrganisationStatusId { get; set; }
        public string? OrganisationStatusDescription { get; set; }
        public int? PrimaryLanguageId { get; set; }
        public string? PrimaryLanguageDescription { get; set; }
        public bool? IsFunded { get; set; }
        public int? EceServiceProviderId { get; set; }
        public string? EceServiceProviderNumber { get; set; }
        public string? EceServiceProviderName { get; set; }
        public int? ServiceProvisionTypeId { get; set; }
        public string? ServiceProvisionTypeDescription { get; set; }
        public int ApplicationStatusId { get; set; }
        public string? ApplicationStatusDescription { get; set; }
        public int LicenceStatusId { get; set; }
        public string? LicenceStatusDescription { get; set; }
        public int? LicenceClassId { get; set; }
        public string? LicenceClassDescription { get; set; }
        public int? EquityIndexId { get; set; }
        public string? EquityIndexDescription { get; set; }
        public decimal? IsolationIndex { get; set; }

        public virtual ICollection<EceOperatingSession> OperatingSessions { get; set; } = null!;

        public bool IsAttestationRequired =>
            OrganisationTypeId == OrganisationType.CasualEducationAndCare ||
            OrganisationTypeId == OrganisationType.EducationAndCare ||
            OrganisationTypeId == OrganisationType.Hospitalbased;

        public bool IsParentLed =>
            OrganisationTypeId switch
            {
                OrganisationType.Playcentre => LicenceClassId != LicenceClass.Mixed,
                _ => false
            };

        public bool IsAllDays =>
            OrganisationTypeId switch
            {
                OrganisationType.CasualEducationAndCare => LicenceClassId != LicenceClass.Sessional,
                OrganisationType.EducationAndCare => LicenceClassId != LicenceClass.Sessional,
                OrganisationType.Hospitalbased => LicenceClassId != LicenceClass.Sessional,
                OrganisationType.FreeKindergarten => LicenceClassId != LicenceClass.Sessional,
                OrganisationType.Playcentre => false,
                OrganisationType.HomebasedNetwork => true,
                _ => true
            };

        public bool IsSessional =>
            OrganisationTypeId switch
            {
                OrganisationType.CasualEducationAndCare => LicenceClassId != LicenceClass.AllDay,
                OrganisationType.EducationAndCare => LicenceClassId != LicenceClass.AllDay,
                OrganisationType.Hospitalbased => LicenceClassId != LicenceClass.AllDay,
                OrganisationType.FreeKindergarten => LicenceClassId != LicenceClass.AllDay,
                OrganisationType.Playcentre => false,
                OrganisationType.HomebasedNetwork => false,
                _ => true
            };

        public int ParentLedMaxFundingDays(int month, int year) =>
            CalculateMaximumFundingDays(IsParentLed, month, year, SessionType.Sessional);

        public int AllDaysMaxFundingDays(int month, int year) =>
            CalculateMaximumFundingDays(IsAllDays, month, year, SessionType.AllDay);

        public int SessionalMaxFundingDays(int month, int year) =>
            CalculateMaximumFundingDays(IsSessional, month, year, SessionType.Sessional);

        public int CalculateMaximumFundingDays(bool isServiceEligible,
            int month, int year,
            int sessionTypeId
        )
        {
            if (isServiceEligible == false)
            {
                return 0;
            }

            if (month < 1 || month > 12 || year < 1 || year > 9999)
            {
                return 0;
            }

            int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);

            EceOperatingSession[]? daysOfWeekWithSessions = OperatingSessions.Where(operatingSession =>
                    operatingSession.SessionTypeId == sessionTypeId)
                .ToArray();

            int availableDays = 0;

            if (!daysOfWeekWithSessions.Any())
            {
                return availableDays;
            }

            for (int dayOfMonth = 1; dayOfMonth <= numberOfDaysInMonth; dayOfMonth++)
            {
                // Get the day of the week eg. Sunday = 0, Monday = 1 etc..
                DayOfWeek dayOfWeek = new DateTime(
                    year,
                    month,
                    dayOfMonth
                ).DayOfWeek;

                // Check whether the day of the week is allowed a session on that day of the week.
                if (daysOfWeekWithSessions.Any(session => session.DayOfWeek == dayOfWeek))
                {
                    availableDays++;
                }
            }

            return availableDays;
        }
    }
}