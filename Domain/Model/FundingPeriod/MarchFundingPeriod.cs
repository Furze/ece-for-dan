using System.Collections.Generic;
using System.Linq;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.FundingPeriod
{
    public class MarchFundingPeriod : FundingPeriod
    {
        public MarchFundingPeriod(Date date) : base(CalendarMonth.March, date)
        {
            FundingPeriodMonths = new[]
            {
                CalendarMonth.February, CalendarMonth.March, CalendarMonth.April, CalendarMonth.May
            };

            if (FundingPeriodMonths.Any(month => month.Id == date.Month) == false)
            {
                throw new ECEApplicationException($"Date - {date} is invalid for {nameof(MarchFundingPeriod)}");
            }
        }

        protected override Dictionary<CalendarMonth, MonthYear[]> CalendarMonthAdvancePeriods =>
            new Dictionary<CalendarMonth, MonthYear[]>
            {
                {
                    CalendarMonth.February,
                    new[]
                    {
                        new MonthYear(CalendarMonth.February, StartDate.Year),
                        new MonthYear(CalendarMonth.March, StartDate.Year),
                        new MonthYear(CalendarMonth.April, StartDate.Year),
                        new MonthYear(CalendarMonth.May, StartDate.Year),
                        new MonthYear(CalendarMonth.June, StartDate.Year)
                    }
                },
                {
                    CalendarMonth.March,
                    new[]
                    {
                        new MonthYear(CalendarMonth.March, StartDate.Year),
                        new MonthYear(CalendarMonth.April, StartDate.Year),
                        new MonthYear(CalendarMonth.May, StartDate.Year),
                        new MonthYear(CalendarMonth.June, StartDate.Year)
                    }
                },
                {
                    CalendarMonth.April,
                    new[]
                    {
                        new MonthYear(CalendarMonth.April, StartDate.Year),
                        new MonthYear(CalendarMonth.May, StartDate.Year),
                        new MonthYear(CalendarMonth.June, StartDate.Year)
                    }
                },
                {
                    CalendarMonth.May,
                    new[]
                    {
                        new MonthYear(CalendarMonth.May, StartDate.Year),
                        new MonthYear(CalendarMonth.June, StartDate.Year)
                    }
                }
            };

        public override FundingPeriod PreviousFundingPeriod
            => new NovemberFundingPeriod(new Date(1, CalendarMonth.November.Id, StartDate.Year - 1));

        public override FundingPeriod NextFundingPeriod
            => new JulyFundingPeriod(new Date(1, CalendarMonth.July.Id, StartDate.Year));
    }
}