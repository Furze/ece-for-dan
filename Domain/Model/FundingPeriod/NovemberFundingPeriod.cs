using System.Collections.Generic;
using System.Linq;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.FundingPeriod
{
    public class NovemberFundingPeriod : FundingPeriod
    {
        public NovemberFundingPeriod(Date date) : base(CalendarMonth.November, date)
        {
            FundingPeriodMonths = new[]
                {CalendarMonth.October, CalendarMonth.November, CalendarMonth.December, CalendarMonth.January};

            if (FundingPeriodMonths.Any(month => month.Id == date.Month) == false)
                throw new ECEApplicationException($"Date - {date} is invalid for {nameof(NovemberFundingPeriod)}");
        }

        /// <inheritdoc />
        protected override Dictionary<CalendarMonth, MonthYear[]> CalendarMonthAdvancePeriods =>
            new Dictionary<CalendarMonth, MonthYear[]>
            {
                {
                    CalendarMonth.October,
                    new[]
                    {
                        new MonthYear(CalendarMonth.October, StartDate.Year),
                        new MonthYear(CalendarMonth.November, StartDate.Year),
                        new MonthYear(CalendarMonth.December, StartDate.Year),
                        new MonthYear(CalendarMonth.January, StartDate.Year + 1),
                        new MonthYear(CalendarMonth.February, StartDate.Year + 1)
                    }
                },
                {
                    CalendarMonth.November,
                    new[]
                    {
                        new MonthYear(CalendarMonth.November, StartDate.Year),
                        new MonthYear(CalendarMonth.December, StartDate.Year),
                        new MonthYear(CalendarMonth.January, StartDate.Year + 1),
                        new MonthYear(CalendarMonth.February, StartDate.Year + 1)
                    }
                },
                {
                    CalendarMonth.December,
                    new[]
                    {
                        new MonthYear(CalendarMonth.December, StartDate.Year),
                        new MonthYear(CalendarMonth.January, StartDate.Year + 1),
                        new MonthYear(CalendarMonth.February, StartDate.Year + 1)
                    }
                },
                {
                    CalendarMonth.January,
                    new[]
                    {
                        new MonthYear(CalendarMonth.January, StartDate.Year + 1),
                        new MonthYear(CalendarMonth.February, StartDate.Year + 1)
                    }
                }
            };

        public override FundingPeriod PreviousFundingPeriod
            => new JulyFundingPeriod(new Date(1, CalendarMonth.July.Id, StartDate.Year));

        public override FundingPeriod NextFundingPeriod
            => new MarchFundingPeriod(new Date(1, CalendarMonth.March.Id, StartDate.Year + 1));

        // public override Date NextPeriodStartDate => new Date(1, CalendarMonth.March.Id, StartDate.Year + 1);
    }
}