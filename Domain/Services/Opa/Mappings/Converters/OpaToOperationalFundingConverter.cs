using System;
using System.Collections.Generic;
using AutoMapper;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Services.Opa.Response;

namespace MoE.ECE.Domain.Services.Opa.Mappings.Converters
{
    public class OpaToOperationalFundingConverter : ITypeConverter<OperationalFundingBaseResponse, FundingRequest>
    {
        public FundingRequest Convert(
            OperationalFundingBaseResponse source,
            FundingRequest destination,
            ResolutionContext context)
        {
            var operationalFunding = new OperationalFundingRequest
            {
                FundingYear = source.FundingYear,
                FundingPeriodMonth = GetFundingPeriodMonth(source.FundingPeriod),
                EntitlementMonths = GetOperationalFundingEntitlementMonths(source),
                AdvanceMonths = GetOperationalFundingAdvanceMonths(source),
                TotalWashUp = source.TotalWashUp,
                TotalAdvance = source.TotalAdvance
            };

            return operationalFunding;
        }

        private ICollection<EntitlementMonthFundingComponent> GetOperationalFundingEntitlementMonths(OperationalFundingBaseResponse source)
        {
            var operationalFundingEntitlementMonths = new List<EntitlementMonthFundingComponent>();
            if (source.EntitlementMonths != null)
            {
                foreach (var opaEntitlementMonth in source.EntitlementMonths)
                {
                    operationalFundingEntitlementMonths.Add(new EntitlementMonthFundingComponent
                    {
                        MonthName = opaEntitlementMonth.MonthName,
                        MonthNumber = opaEntitlementMonth.MonthNumber,
                        Year = opaEntitlementMonth.Year,
                        AllDayCertificatedTeacherHours = opaEntitlementMonth.AllDayCertificatedTeacherHours,
                        AllDayNonCertificatedTeacherHours = opaEntitlementMonth.AllDayNonCertificatedTeacherHours,
                        SessionalCertificatedTeacherHours = opaEntitlementMonth.SessionalCertificatedTeacherHours,
                        SessionalNonCertificatedTeacherHours = opaEntitlementMonth.SessionalNonCertificatedTeacherHours,
                        TotalWorkingDays = opaEntitlementMonth.TotalWorkingDays,
                        WashUpPlusTen = opaEntitlementMonth.WashUpPlusTen,
                        WashUpUnderTwo = opaEntitlementMonth.WashUpUnderTwo,
                        WashUpTwoAndOver = opaEntitlementMonth.WashUpTwoAndOver,
                        WashUpTwentyHours = opaEntitlementMonth.WashUpTwentyHours,
                        TotalWashUp = opaEntitlementMonth.TotalWashUp,
                        TotalEntitlement = opaEntitlementMonth.TotalEntitlement,
                        TotalFundsAdvanced = opaEntitlementMonth.TotalFundsAdvanced,
                        TotalEntitlementPlusTen = opaEntitlementMonth.TotalEntitlementPlusTen,
                        TotalEntitlementTwentyHours = opaEntitlementMonth.TotalEntitlementTwentyHours,
                        TotalEntitlementTwoAndOver = opaEntitlementMonth.TotalEntitlementTwoAndOver,
                        TotalEntitlementUnderTwo = opaEntitlementMonth.TotalEntitlementUnderTwo,
                        EntitlementFundingComponents = GetEntitlementFundingComponents(opaEntitlementMonth.EntitlementAmounts)
                    });
                }
            }

            return operationalFundingEntitlementMonths;
        }

        private ICollection<AdvanceMonthFundingComponent> GetOperationalFundingAdvanceMonths(OperationalFundingBaseResponse source)
        {
            var operationalFundingAdvancedMonths = new List<AdvanceMonthFundingComponent>();

            if (source.AdvanceMonths != null)
            {
                foreach (var opaAdvanceMonth in source.AdvanceMonths)
                {
                    operationalFundingAdvancedMonths.Add(new AdvanceMonthFundingComponent
                    {
                        MonthName = opaAdvanceMonth.MonthName,
                        MonthNumber = opaAdvanceMonth.MonthNumber,
                        Year = opaAdvanceMonth.Year,
                        AllDayWorkingDays = opaAdvanceMonth.EstimateAllDayDays,
                        SessionalWorkingDays = opaAdvanceMonth.EstimateSessionalDays,
                        ParentLedWorkingDays = opaAdvanceMonth.EstimateParentLedDays,
                        AdvanceFundingComponents = GetAdvanceFundingComponents(opaAdvanceMonth.AdvanceAmounts),
                        AmountPayableTwentyHours = opaAdvanceMonth.PayableTwentyHours,
                        AmountPayableTwoAndOver = opaAdvanceMonth.PayableTwoAndOver,
                        AmountPayablePlusTen = opaAdvanceMonth.PayablePlusTen,
                        AmountPayableUnderTwo = opaAdvanceMonth.PayableUnderTwo,
                        TotalDays = opaAdvanceMonth.TotalDays,
                        TotalAdvance = opaAdvanceMonth.TotalAdvance
                    });
                }
            }

            return operationalFundingAdvancedMonths;
        }

        private ICollection<EntitlementFundingComponent> GetEntitlementFundingComponents(ICollection<EntitlementAmount>? entitlementAmounts)
        {
            var results = new List<EntitlementFundingComponent>();
            if (entitlementAmounts != null)
            {
                foreach (var entitlement in entitlementAmounts)
                {
                    var sessionType = GetSessionType(entitlement.SessionType);
                    if (sessionType != Session.Unknown)
                    {
                        results.Add(new EntitlementFundingComponent
                        {
                            SessionTypeId = sessionType,
                            FundingComponentTypeId = GetFundingComponent(entitlement.ComponentType),
                            StartDate = entitlement.StartDate,
                            Amount = entitlement.Amount,
                            Rate = entitlement.Rate,
                            RateName = entitlement.RateName,
                            FundedChildHours = entitlement.Fch,
                            OperatingDays = entitlement.Days
                        });
                    }
                }
            }

            return results;
        }

        private ICollection<AdvanceFundingComponent> GetAdvanceFundingComponents(ICollection<AdvanceAmount>? advanceAmounts)
        {
            var results = new List<AdvanceFundingComponent>();
            if (advanceAmounts != null)
            {
                foreach (var advance in advanceAmounts)
                {
                    var sessionType = GetSessionType(advance.SessionType);
                    if (sessionType != Session.Unknown)
                    {
                        results.Add(new AdvanceFundingComponent
                        {
                            SessionTypeId = sessionType,
                            FundingComponentTypeId = GetFundingComponent(advance.ComponentType),
                            StartDate = advance.StartDate,
                            Amount = advance.Amount,
                            Rate = advance.Rate,
                            RateName = advance.RateName,
                            FundedChildHours = advance.Fch,
                            OperatingDays = advance.Days
                        });
                    }
                }
            }

            return results;
        }

        private FundingPeriodMonth? GetFundingPeriodMonth(int period)
        {
            switch (period)
            {
                case 1:
                    return FundingPeriodMonth.July;

                case 2:
                    return FundingPeriodMonth.November;

                case 3:
                    return FundingPeriodMonth.March;

                default:
                    throw new ApplicationException($"No funding period month found for period {period}");
            }
        }

        private FundingComponent? GetFundingComponent(string? component)
        {
            switch (component)
            {
                case "Under 2":
                    return FundingComponent.UnderTwo;

                case "2 And Over":
                    return FundingComponent.TwoAndOver;

                case "Plus 10":
                    return FundingComponent.PlusTen;

                case "20 Hours":
                    return FundingComponent.TwentyHours;

                default:
                    return FundingComponent.Unknown;
            }
        }

        private Session? GetSessionType(string? session)
        {
            switch (session)
            {
                case "All Day":
                    return Session.AllDay;

                case "Sessional":
                    return Session.Sessional;

                default:
                    return Session.Unknown;
            }
        }
    }
}