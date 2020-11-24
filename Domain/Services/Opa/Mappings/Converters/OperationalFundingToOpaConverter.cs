using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Marten;
using Microsoft.EntityFrameworkCore;
using MoE.ECE.Domain.Command;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Infrastructure.Filters;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Model.ReferenceData;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Services.Opa.Request;
using ServiceProfile = MoE.ECE.Domain.Services.Opa.Request.ServiceProfile;

namespace MoE.ECE.Domain.Services.Opa.Mappings.Converters
{
    public partial class OperationalFundingToOpaConverter : ITypeConverter<CreateOperationalFundingRequest,
        OpaRequest<OperationalFundingBaseRequest>>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ReferenceDataContext _referenceDataContext;

        private int _opaEntitlementDaysId = 1;
        private int _opaServiceProfilesId = 1;

        public OperationalFundingToOpaConverter(
            ReferenceDataContext referenceDataContext,
            IDocumentSession documentSession)
        {
            _referenceDataContext = referenceDataContext;
            _documentSession = documentSession;
        }

        private static IEnumerable<string> OpaOperationalFundingOutcomeParameters =>
            new List<string>
            {
                "entMonthName",
                "relEntitlement",
                "entitlementAmountName",
                "entitlementAmountComponentType",
                "entitlementAmountSessionType",
                "entitlementAmount",
                "entitlementAmountFCH",
                "entitlementAmountRate",
                "entitlementAmountStartDate",
                "entitlementAmountDays",
                "entitlementAmountRateName",
                "advMonthName",
                "advMonthNumber",
                "advanceAmountName",
                "advanceAmountComponentType",
                "advanceAmountStartDate",
                "advanceAmountDays",
                "advanceAmountFCH",
                "advanceAmountRate",
                "advanceAmountRateName",
                "advanceAmount",
                "advMonthEstimateAllDayDays",
                "advMonthEstimateSessionalDays",
                "advMonthEstimateParentLedDays",
                "advMonthAmountPayableUnder2",
                "advMonthAmountPayable2AndOver",
                "advMonthAmountPayable20Hours",
                "advMonthAmountPayablePlus10",
                "eceAllDayCertificatedTeacherPercentage",
                "eceSessionalCertificatedTeacherPercentage",
                "fundingYear",
                "fundingPeriod",
                "isLicenceExempt",
                "entMonthNumber",
                "entMonthSessionalCertificatedTeacherHours",
                "entMonthSessionalNonCertificatedTeacherHours",
                "entMonthAllDayNonCertificatedTeacherHours",
                "entMonthAllDayCertificatedTeacherHours",
                "entMonthWashUp20Hours",
                "entMonthWashUpPlus10",
                "entMonthWashUp2AndOver",
                "entMonthWashUpUnder2",
                "advanceAmountSessionType",
                "entMonthTotalWorkingDays",
                "advMonthTotalDays",
                "entMonthTotalWashUp",
                "eceTotalWashUp",
                "entMonthYear",
                "advMonthYear",
                "entMonthTotalEntitlement",
                "entMonthTotalFundsAdvanced",
                "entMonthEntitlementUnder2",
                "entMonthEntitlement2andOver",
                "entMonthEntitlement20Hours",
                "entMonthEntitlementPlus10",
                "eceTotalAdvance",
                "advMonthTotalAdvance"
            };

        public OpaRequest<OperationalFundingBaseRequest> Convert(
            CreateOperationalFundingRequest source,
            OpaRequest<OperationalFundingBaseRequest> destination,
            ResolutionContext context)
        {
            var eceService = _referenceDataContext.EceServices
                .Include(service => service.OperatingSessions)
                .SingleOrDefault(service =>
                    service.RefOrganisationId == source.OrganisationId);

            if (eceService == null)
            {
                throw new ApplicationException($"Ece service '{source.OrganisationId} could not be found");
            }

            const int opaCaseId = 1;
            return new OpaRequest<OperationalFundingBaseRequest>
            {
                Outcomes = OpaOperationalFundingOutcomeParameters,
                Cases = new List<OperationalFundingBaseRequest>
                {
                    new OperationalFundingBaseRequest
                    {
                        Id = $"Case {opaCaseId}",
                        ApplicationType = OpaConstants.OpaApplicationType.Rs7,
                        IsAttested = source.IsAttested ? "Y" : "N",
                        FundingYear = source.FundingYear,
                        FundingPeriod = GetOpaFundingPeriod(source.FundingPeriodMonth),
                        EntitlementMonths = GetOpaEntitlementMonths(source),
                        AdvanceMonths = GetOpaAdvanceMonths(source),
                        ServiceProfiles = GetServiceProfiles(eceService)
                    }
                }
            };
        }

        private ICollection<EntitlementMonth> GetOpaEntitlementMonths(CreateOperationalFundingRequest source)
        {
            var opaEntitlementMonths = new List<EntitlementMonth>();
            var opaId = 1;

            if (source.EntitlementMonths == null)
            {
                return opaEntitlementMonths;
            }

            foreach (var entitlementMonth in source.EntitlementMonths)
            {
                var matchingAdvanceMonth = _documentSession.Query<OperationalFundingRequest>()
                    .GetEntitlementAdvancedMonth(source.OrganisationId, entitlementMonth.Year,
                        entitlementMonth.MonthNumber);

                opaEntitlementMonths.Add(new EntitlementMonth
                {
                    Id = $"relEntitlementMonth- {opaId}",
                    MonthNumber = entitlementMonth.MonthNumber,
                    MonthFundsAdvancedUnder2 = matchingAdvanceMonth?.AmountPayableUnderTwo,
                    MonthFundsAdvanced2AndOver = matchingAdvanceMonth?.AmountPayableTwoAndOver,
                    MonthFundsAdvanced20Hours = matchingAdvanceMonth?.AmountPayableTwentyHours,
                    MonthFundsAdvancedPlus10 = matchingAdvanceMonth?.AmountPayablePlusTen,
                    EntitlementDays = GetOpaEntitlementDays(entitlementMonth)
                });
                opaId++;
            }

            return opaEntitlementMonths;
        }

        private ICollection<EntitlementDay> GetOpaEntitlementDays(Rs7EntitlementMonth rs7EntitlementMonth)
        {
            var opaEntitlementDays = new List<EntitlementDay>();

            foreach (var submittedDay in rs7EntitlementMonth.Days)
            {
                opaEntitlementDays.Add(new EntitlementDay
                {
                    Id = $"relEntitlementDay {_opaEntitlementDaysId}",
                    Number = submittedDay.DayNumber,
                    FCHUnder2 = submittedDay.Under2,
                    FCH2AndOver = submittedDay.TwoAndOver,
                    FCH20Hours = submittedDay.Hours20,
                    FCHPlus10 = submittedDay.Plus10,
                    CertificatedTeacherHours = submittedDay.Certificated,
                    NonCertificatedTeacherHours = submittedDay.NonCertificated
                });
                _opaEntitlementDaysId++;
            }

            return opaEntitlementDays;
        }

        private static ICollection<AdvanceMonth> GetOpaAdvanceMonths(CreateOperationalFundingRequest source)
        {
            var opaAdvanceMonths = new List<AdvanceMonth>();
            var opaId = 1;

            if (source.AdvanceMonths == null)
            {
                return opaAdvanceMonths;
            }

            foreach (var submittedAdvanceMonth in source.AdvanceMonths)
            {
                opaAdvanceMonths.Add(new AdvanceMonth
                {
                    Id = opaId,
                    MonthNumber = submittedAdvanceMonth.MonthNumber,
                    AllDayWorkingDays = submittedAdvanceMonth.AllDay,
                    SessionalWorkingDays = submittedAdvanceMonth.Sessional,
                    ParentLedWorkingDays = submittedAdvanceMonth.ParentLed
                });
                opaId++;
            }

            return opaAdvanceMonths;
        }

        private ICollection<ServiceProfile> GetServiceProfiles(EceService eceService)
        {
            var serviceProfiles = new List<ServiceProfile>();

            var serviceHistory = _referenceDataContext.EceServiceDateRangedParameters
                .GetHistory(eceService.RefOrganisationId, HistoryAttribute.OrganisationType,
                    HistoryAttribute.QualityLevel, HistoryAttribute.EquityIndex,
                    HistoryAttribute.IsolationIndex, HistoryAttribute.PrimaryLanguage);

            void AddServiceProfile(string historyAttribute, string profileType, string profileValue)
            {
                var history = serviceHistory.Where(parameter => parameter.Attribute == historyAttribute).ToArray();

                if (history.Any())
                {
                    serviceProfiles.AddRange(history
                        .Select(entity => GetServiceProfile(
                            profileType,
                            entity.Value,
                            entity.EffectiveFromDate.ToNzDateTimeOffSet(),
                            entity.EffectiveToDate?.ToNzDateTimeOffSet())));
                }
                else
                {
                    serviceProfiles.Add(GetServiceProfile(profileType, profileValue));
                }
            }

            AddServiceProfile(HistoryAttribute.OrganisationType, OpaServiceProfileType.OrganisationType,
                eceService.OrganisationTypeId.ToString());
            AddServiceProfile(HistoryAttribute.QualityLevel, OpaServiceProfileType.QualityLevel,
                eceService.EcQualityLevelId.GetValueOrDefault().ToString());
            AddServiceProfile(HistoryAttribute.EquityIndex, OpaServiceProfileType.EquityIndex,
                eceService.EquityIndexId.GetValueOrDefault().ToString());
            AddServiceProfile(HistoryAttribute.IsolationIndex, OpaServiceProfileType.IsolationIndex,
                eceService.IsolationIndex.GetValueOrDefault().ToString(CultureInfo.InvariantCulture));
            AddServiceProfile(HistoryAttribute.PrimaryLanguage, OpaServiceProfileType.PrimaryLanguage,
                eceService.PrimaryLanguageId.GetValueOrDefault().ToString());

            AddServiceProvisionHistory(eceService, serviceProfiles);
            AddOperatingSessionHistory(eceService, serviceProfiles);

            return serviceProfiles;
        }

        private void AddServiceProvisionHistory(EceService eceService, List<ServiceProfile> serviceProfiles)
        {
            var history = _referenceDataContext.EceLicencingDetailDateRangedParameters
                .GetLicencingDetailHistory(eceService.RefOrganisationId)
                .ToList();

            if (history.Any())
            {
                serviceProfiles.AddRange(history
                    .Select(entity => GetServiceProfile(
                        OpaServiceProfileType.ServiceProvision,
                        entity.ServiceProvisionTypeId.ToString(),
                        entity.EffectiveFromDate.ToNzDateTimeOffSet(),
                        entity.EffectiveToDate?.ToNzDateTimeOffSet()))
                    .ToList());
            }
            else
            {
                serviceProfiles.Add(
                    GetServiceProfile(OpaServiceProfileType.ServiceProvision,
                        eceService.ServiceProvisionTypeId.ToString()));
            }
        }

        private void AddOperatingSessionHistory(EceService eceService, List<ServiceProfile> serviceProfiles)
        {
            var operatingSessions = new Dictionary<int, OperatingSession>
            {
                {
                    1,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.MondaySessionType,
                        HistoryAttribute = SessionDay.Monday,
                        ServiceValue = eceService.MondaySessionType
                    }
                },
                {
                    2,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.TuesdaySessionType,
                        HistoryAttribute = SessionDay.Tuesday,
                        ServiceValue = eceService.TuesdaySessionType
                    }
                },
                {
                    3,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.WednesdaySessionType,
                        HistoryAttribute = SessionDay.Wednesday,
                        ServiceValue = eceService.WednesdaySessionType
                    }
                },
                {
                    4,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.ThursdaySessionType,
                        HistoryAttribute = SessionDay.Thursday,
                        ServiceValue = eceService.ThursdaySessionType
                    }
                },
                {
                    5,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.FridaySessionType,
                        HistoryAttribute = SessionDay.Friday,
                        ServiceValue = eceService.FridaySessionType
                    }
                },
                {
                    6,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.SaturdaySessionType,
                        HistoryAttribute = SessionDay.Saturday,
                        ServiceValue = eceService.SaturdaySessionType
                    }
                },
                {
                    7,
                    new OperatingSession
                    {
                        ProfileType = OpaServiceProfileType.SundaySessionType,
                        HistoryAttribute = SessionDay.Sunday,
                        ServiceValue = eceService.SundaySessionType
                    }
                }
            };

            foreach (var index in Enumerable.Range(1, 7))
            {
                var history = _referenceDataContext.EceOperatingSessionDateRangedParameters
                    .GetOperatingSessionHistory(eceService.RefOrganisationId, operatingSessions[index].HistoryAttribute)
                    .ToList();

                if (history.Count == 0)
                {
                    serviceProfiles.Add(
                        GetServiceProfile(operatingSessions[index].ProfileType,
                            operatingSessions[index].ServiceValue.ToString()));
                }
                else
                {
                    serviceProfiles.AddRange(history
                        .GroupBy(entity => new
                        {
                            entity.SessionDayId,
                            entity.SessionTypeId,
                            entity.SessionProvisionTypeId,
                            entity.EffectiveFromDate,
                            entity.EffectiveToDate
                        })
                        .Select(entity => GetServiceProfile(
                            operatingSessions[index].ProfileType,
                            entity.First().SessionTypeId.ToString(),
                            entity.First().EffectiveFromDate.ToNzDateTimeOffSet(),
                            entity.First().EffectiveToDate?.ToNzDateTimeOffSet()))
                        .ToList());
                }
            }
        }

        private ServiceProfile GetServiceProfile(
            string profileType,
            string? value,
            DateTimeOffset? fromDate = null,
            DateTimeOffset? toDate = null) =>
            new ServiceProfile
            {
                Id = $"relServiceProfile-{_opaServiceProfilesId++}",
                Description = profileType,
                ValueID = value,
                EffectiveFromDate = fromDate?.ToNzDateTimeOffSet(),
                EffectiveToDate = toDate?.ToNzDateTimeOffSet()
            };

        private static int? GetOpaFundingPeriod(FundingPeriodMonth? fundingPeriodMonth) =>
            fundingPeriodMonth switch
            {
                FundingPeriodMonth.March => 3,
                FundingPeriodMonth.July => 1,
                FundingPeriodMonth.November => 2,
                var _ => null
            };
    }

    public class OperatingSession
    {
        public string ProfileType { get; set; } = string.Empty;
        public string HistoryAttribute { get; set; } = string.Empty;
        public decimal? ServiceValue { get; set; }
    }
}