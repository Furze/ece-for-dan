using System;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    public class EceOperatingSessionDateRangedParameterReferenceData
    {
        private const string ParentLedServiceProvisionTypeDescription = "Parent Led";

        public EceOperatingSessionDateRangedParameter[] Data
        {
            get
            {
                return new[]
                {
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 1,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 2,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(2020, 01, 31, 11, 00, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 5,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(2020, 01, 31, 11, 00, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 6,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(2020, 01, 31, 11, 00, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 7,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(2020, 01, 31, 11, 00, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 3,
                        RefOrganisationId = 9933,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceOperatingSessionDateRangedParameter
                    {
                        LicencingDetailHistoryId = 4,
                        RefOrganisationId = 9933,
                        SessionDayDescription = SessionDay.Monday,
                        SessionTypeId = 25000,
                        SessionTypeDescription = "All Day",
                        SessionProvisionTypeId = 136001,
                        SessionProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    }
                };
            }
        }
    }
}