using System;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    public class EceServiceDateRangedParameterReferenceData
    {
        private const string TeKohangaReoTypeDescription = "Te Kohanga Reo";
        private const string LicensedEceServiceAttributeSource = "LicensedECEServiceResourcingParameterHistory";

        public EceServiceDateRangedParameter[] Data
        {
            get
            {
                return new[]
                {
                    new EceServiceDateRangedParameter
                    {
                        HistoryId = 1,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        AttributeSource = "OrganisationHistory",
                        Attribute = HistoryAttribute.OrganisationType,
                        Value = OrganisationType.TeKohangaReo.ToString(),
                        ValueDescription = TeKohangaReoTypeDescription,
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceServiceDateRangedParameter
                    {
                        HistoryId = 2,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        AttributeSource = "ECEServiceHistory",
                        Attribute = HistoryAttribute.PrimaryLanguage,
                        Value = "15001",
                        ValueDescription = "English",
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceServiceDateRangedParameter
                    {
                        HistoryId = 3,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        AttributeSource = LicensedEceServiceAttributeSource,
                        Attribute = HistoryAttribute.EquityIndex,
                        Value = "19001",
                        ValueDescription = "EQ 1",
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceServiceDateRangedParameter
                    {
                        HistoryId = 4,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        AttributeSource = LicensedEceServiceAttributeSource,
                        Attribute = HistoryAttribute.IsolationIndex,
                        Value = "1.93",
                        ValueDescription = null,
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    },
                    new EceServiceDateRangedParameter
                    {
                        HistoryId = 5,
                        RefOrganisationId = ReferenceData.EceServices.TeKohangaReoOWaikare.RefOrganisationId,
                        AttributeSource = LicensedEceServiceAttributeSource,
                        Attribute = HistoryAttribute.QualityLevel,
                        Value = "21001",
                        ValueDescription = QualityLevel.QualityLevelTKR,
                        EffectiveFromDate = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                        EffectiveToDate = null
                    }
                };
            }
        }
    }
}