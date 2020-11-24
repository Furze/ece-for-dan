using System;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    public class EceLicencingDetailDateRangedParameterReferenceData
    {
        private const string ParentLedServiceProvisionTypeDescription = "Parent Led";

        public EceLicencingDetailDateRangedParameter[] Data
        {
            get
            {
                return new[]
                {
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 1,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Withdrawn
                    },
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 2,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Endorsed
                    },
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 5,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Recommended
                    },
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 6,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Received
                    },
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 3,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Endorsed
                    },
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 4,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Endorsed
                    },
                    new EceLicencingDetailDateRangedParameter
                    {
                        LicencingDetailHistoryId = 7,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        EffectiveFromDate = new DateTimeOffset(1899, 12, 30, 6, 30, 0, DateHelper.NzTimeZone.BaseUtcOffset),
                        EffectiveToDate = null,
                        ApplicationStatusId = ApplicationStatus.Endorsed
                    }
                };
            }
        }
    }
}