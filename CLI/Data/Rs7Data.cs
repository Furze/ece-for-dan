using System;
using System.Linq;
using MoE.ECE.Domain.Model.FundingPeriod;
using MoE.ECE.Domain.Model.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using Date = MoE.ECE.Domain.Model.ValueObject.Date;

namespace MoE.ECE.CLI.Data
{
    public class Rs7Data
    {
        public Rs7[] Data
        {
            get
            {
                var julyFundingPeriod = FundingPeriod.GetFundingPeriodForDate(new Date(1, (int) FundingPeriodMonth.July, DateTime.Now.Year - 1));
                var novemberFundingPeriod = FundingPeriod.GetFundingPeriodForDate(new Date(1, (int) FundingPeriodMonth.November, DateTime.Now.Year - 1));
                return new[]
                {
                    new Rs7
                    {
                        FundingPeriod = (FundingPeriodMonth) julyFundingPeriod.StartDate.Month,
                        FundingYear = julyFundingPeriod.FundingYear,
                        FundingPeriodYear = julyFundingPeriod.FundingPeriodYear,
                        OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                        RollStatus = RollStatus.ExternalDraft,
                        Revisions = new[]
                        {
                            new Rs7Revision
                            {
                                RevisionNumber = 1,
                                RevisionDate = DateTimeOffset.Now,
                                AdvanceMonths = CreateAdvanceMonths(julyFundingPeriod.StartDate),
                                EntitlementMonths = CreateEntitlementMonths(julyFundingPeriod.StartDate),
                                IsAttested = false
                            }
                        }
                    },
                    new Rs7
                    {
                        FundingPeriod = (FundingPeriodMonth) novemberFundingPeriod.StartDate.Month,
                        FundingYear = novemberFundingPeriod.FundingYear,
                        FundingPeriodYear = novemberFundingPeriod.FundingPeriodYear,
                        OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                        RollStatus = RollStatus.InternalApproved,
                        Revisions = new[]
                        {
                            new Rs7Revision
                            {
                                RevisionNumber = 1,
                                RevisionDate = DateTimeOffset.Now,
                                AdvanceMonths = CreateAdvanceMonths(novemberFundingPeriod.StartDate),
                                EntitlementMonths = CreateEntitlementMonths(novemberFundingPeriod.StartDate),
                                IsAttested = true
                            }
                        }
                    }
                };
            }
        }

        private static Rs7AdvanceMonth[] CreateAdvanceMonths(Date monthDate)
        {
            var date = new DateTime(monthDate.Year, monthDate.Month, 1);
            return Enumerable.Range(1, 4).Select(monthOffset =>
            {
                var advanceMonth = date.AddMonths(monthOffset);
                return new Rs7AdvanceMonth
                {
                    MonthNumber = advanceMonth.Month,
                    Year = advanceMonth.Year,
                    AllDay = 41,
                    ParentLed = 21,
                    Sessional = 0
                };
            }).ToArray();
        }

        private static Rs7EntitlementMonth[] CreateEntitlementMonths(Date monthDate)
        {
            var date = new DateTime(monthDate.Year, monthDate.Month, 1);
            return Enumerable.Range(-4, 4)
                .Select(monthOffset => CreateEntitlementMonth(date.AddMonths(monthOffset))).ToArray();
        }

        private static Rs7EntitlementMonth CreateEntitlementMonth(DateTime monthDate)
        {
            var monthDayCount = monthDate.AddMonths(1).AddDays(-1).Day;

            var entitlementMonth = new Rs7EntitlementMonth
                {MonthNumber = monthDate.Month, Year = monthDate.Year};

            foreach (var day in Enumerable.Range(1, monthDayCount))
                entitlementMonth.AddDay(
                    new Rs7EntitlementDay
                    {
                        DayNumber = day,
                        Under2 = 1,
                        TwoAndOver = 2,
                        Hours20 = 3,
                        Plus10 = 4,
                        Certificated = 5,
                        NonCertificated = 6
                    }
                );

            return entitlementMonth;
        }
    }
}