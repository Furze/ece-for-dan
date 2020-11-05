using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7Revision
    {
        public int Id { get; set; }

        public int Rs7Id { get; set; }

        /// <summary>
        /// First value is 1.
        /// </summary>
        public int RevisionNumber { get; set; }

        public DateTimeOffset RevisionDate { get; set; }

        public virtual Rs7 Rs7 { get; set; } = null!;

        public virtual ICollection<Rs7EntitlementMonth> EntitlementMonths { get; set; } = new HashSet<Rs7EntitlementMonth>();

        public virtual ICollection<Rs7AdvanceMonth> AdvanceMonths { get; set; } = new HashSet<Rs7AdvanceMonth>();

        public bool? IsAttested { get; set; }

        public bool IsZeroReturn { get; set; }

        public string Source { get; set; } = string.Empty;

        public virtual Declaration Declaration { get; set; } = null!;

        public RollStatus CurrentRollStatus()
        {
            return Rs7.RollStatus;
        }

        /// <summary>
        /// Creates all the required entitlement and advance months/days for this rs7.
        /// </summary>
        public void CreateMonthsForPeriod()
        {
            CreateEntitlementMonths();
            CreateAdvanceMonths();
        }

        public DateTime AdvanceStartDate
        {
            get
            {
                var date = new DateTime(
                    year: Rs7.FundingPeriodYear,
                    month: (int) Rs7.FundingPeriod,
                    day: 01);

                return date;
            }
        }

        private void CreateEntitlementMonths()
        {
            var date = AdvanceStartDate
                // take off 5 months so we're at the beginning of the washup (advanced months) for
                // the funding period.
                .AddMonths(-5);

            AddMonths(date, 4, entitlementMonth =>
            {
                var month = new Rs7EntitlementMonth
                {
                    Rs7Revision = this,
                    MonthNumber = entitlementMonth.Month,
                    Year = entitlementMonth.Year
                };

                int numberOfDays = entitlementMonth.AddMonths(1).AddDays(-1).Day;

                foreach (int day in Enumerable.Range(1, numberOfDays))
                {
                    month.AddDay(new Rs7EntitlementDay
                    {
                        DayNumber = day
                    });
                }

                EntitlementMonths.Add(month);
            });
        }

        private void CreateAdvanceMonths()
        {
            AddMonths(AdvanceStartDate, 4, advanceMonth =>
            {
                var month = new Rs7AdvanceMonth
                {
                    MonthNumber = advanceMonth.Month,
                    Year = advanceMonth.Year
                };

                AdvanceMonths.Add(month);
            });
        }

        private static void AddMonths(DateTime date, int numberOfMonths, Action<DateTime> monthAction)
        {
            foreach (int offset in Enumerable.Range(0, numberOfMonths))
            {
                monthAction(date.AddMonths(offset));
            }
        }

        public void UpdateRevisionDate(ISystemClock systemClock)
        {
            RevisionDate = systemClock.UtcNow;
        }
    }
}