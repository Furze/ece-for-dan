using System.Collections.Generic;
using System.Linq;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.FundingPeriod
{
    public class JulyFundingPeriod : FundingPeriod
    {
        public JulyFundingPeriod(Date date) : base(CalendarMonth.July, date)
        {
            FundingPeriodMonths = new[]
            {
                CalendarMonth.June, CalendarMonth.July, CalendarMonth.August, CalendarMonth.September
            };

            if (FundingPeriodMonths.Any(month => month.Id == date.Month) == false)
            {
                throw new ECEApplicationException($"Date - {date} is invalid for {nameof(JulyFundingPeriod)}");
            }
        }

        protected override Dictionary<CalendarMonth, MonthYear[]> CalendarMonthAdvancePeriods =>
            new Dictionary<CalendarMonth, MonthYear[]>
            {
                {
                    CalendarMonth.June,
                    new[]
                    {
                        new MonthYear(CalendarMonth.June, StartDate.Year),
                        new MonthYear(CalendarMonth.July, StartDate.Year),
                        new MonthYear(CalendarMonth.August, StartDate.Year),
                        new MonthYear(CalendarMonth.September, StartDate.Year),
                        new MonthYear(CalendarMonth.October, StartDate.Year)
                    }
                },
                {
                    CalendarMonth.July,
                    new[]
                    {
                        new MonthYear(CalendarMonth.July, StartDate.Year),
                        new MonthYear(CalendarMonth.August, StartDate.Year),
                        new MonthYear(CalendarMonth.September, StartDate.Year),
                        new MonthYear(CalendarMonth.October, StartDate.Year)
                    }
                },
                {
                    CalendarMonth.August,
                    new[]
                    {
                        new MonthYear(CalendarMonth.August, StartDate.Year),
                        new MonthYear(CalendarMonth.September, StartDate.Year),
                        new MonthYear(CalendarMonth.October, StartDate.Year)
                    }
                },
                {
                    CalendarMonth.September,
                    new[]
                    {
                        new MonthYear(CalendarMonth.September, StartDate.Year),
                        new MonthYear(CalendarMonth.October, StartDate.Year)
                    }
                }
            };

        public override Date NextPeriodStartDate => new Date(1, CalendarMonth.November.Id, StartDate.Year);
    }
}