using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.FundingPeriod
{
    public abstract class FundingPeriod
    {
        // Factory functions to create the correct Funding Period Type.
        private static readonly Func<Date, FundingPeriod> MarchFundingPeriod = date => new MarchFundingPeriod(date);
        private static readonly Func<Date, FundingPeriod> JulyFundingPeriod = date => new JulyFundingPeriod(date);

        private static readonly Func<Date, FundingPeriod> NovemberFundingPeriod =
            date => new NovemberFundingPeriod(date);

        /// <summary>
        ///     Dictionary that maps month to FundingPeriod factory. By calling the index we get back the correct
        ///     funding factory back
        /// </summary>
        private static readonly Dictionary<CalendarMonth, Func<Date, FundingPeriod>> FundingPeriodFactories =
            new Dictionary<CalendarMonth, Func<Date, FundingPeriod>>
            {
                {CalendarMonth.January, NovemberFundingPeriod},
                {CalendarMonth.February, MarchFundingPeriod},
                {CalendarMonth.March, MarchFundingPeriod},
                {CalendarMonth.April, MarchFundingPeriod},
                {CalendarMonth.May, MarchFundingPeriod},
                {CalendarMonth.June, JulyFundingPeriod},
                {CalendarMonth.July, JulyFundingPeriod},
                {CalendarMonth.August, JulyFundingPeriod},
                {CalendarMonth.September, JulyFundingPeriod},
                {CalendarMonth.October, NovemberFundingPeriod},
                {CalendarMonth.November, NovemberFundingPeriod},
                {CalendarMonth.December, NovemberFundingPeriod}
            };

        protected FundingPeriod(CalendarMonth calendarMonth, Date effectiveDate)
        {
            FundingPeriodMonths = new CalendarMonth[0];
            StartDate = new Date(1, calendarMonth.Id, CalculateFundingPeriodYearFromDate(effectiveDate));
            EffectiveDate = effectiveDate;
            FundingYear = GetFundingYearForFundingPeriod((FundingPeriodMonth) StartDate.Month, StartDate.Year);
            FundingPeriodYear = StartDate.Year;
        }

        /// <summary>
        ///     The date of the Funding Round
        ///     <remarks>Should always be the first of the month</remarks>
        /// </summary>
        public Date StartDate { get; set; }

        // The effective date that finding has been requested for.
        public Date EffectiveDate { get; }

        /// <summary>
        ///     The Financial funding year for the funding period
        /// </summary>
        public int FundingYear { get; set; }

        /// <summary>
        ///     The actual calendar year for the funding period
        /// </summary>
        public int FundingPeriodYear { get; set; }

        /// <summary>
        ///     A collection of the calendar months that can be provided. This will change based on the supplied date
        /// </summary>
        public IReadOnlyCollection<MonthYear> AdvancePeriods =>
            CalendarMonthAdvancePeriods[CalendarMonth.GetById(EffectiveDate.Month)];

        /// <summary>
        ///     The Calendar Months that the funding periods covers
        /// </summary>
        protected CalendarMonth[] FundingPeriodMonths { get; set; }

        protected abstract Dictionary<CalendarMonth, MonthYear[]> CalendarMonthAdvancePeriods { get; }

        public abstract FundingPeriod PreviousFundingPeriod { get; }

        /// <summary>
        ///     This is the date of the next funding period
        /// </summary>
        public abstract FundingPeriod NextFundingPeriod { get; }
        
        /// <summary>
        ///     If the funding period is March then the funding year is the same as the calendar year
        ///     Else it will be the next year
        /// </summary>
        /// <param name="fundingPeriodMonth">The funding period for which we want to calculate the funding year for</param>
        /// <param name="year">The calendar year of the funding period</param>
        /// <returns>the funding year</returns>
        public static int GetFundingYearForFundingPeriod(FundingPeriodMonth fundingPeriodMonth, int year)
        {
            return fundingPeriodMonth == FundingPeriodMonth.March ? year : year + 1;
        }

        /// <summary>
        ///     Factory method to create the funding period for a give date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static FundingPeriod GetFundingPeriodForDate(Date date)
        {
            var instantiateFundingPeriod = FundingPeriodFactories[CalendarMonth.GetById(date.Month)];

            var fundingPeriod = instantiateFundingPeriod(date);

            return fundingPeriod;
        }

        public FundingPeriod EarlierFundingPeriod(int count)
        {
            FundingPeriod fundingPeriod = this;
            for (var i = 0; i < count; i++) fundingPeriod = fundingPeriod.PreviousFundingPeriod;

            return fundingPeriod;
        }

        /// <summary>
        ///     Most of the time the year is the same as the date's year. But when the date is for January then it
        ///     falls into the previous years funding round.
        ///     Returns the actual year for the period (not the financial funding year)
        /// </summary>
        /// <param name="date">The date we wish to create a funding period for.</param>
        /// <returns>the year</returns>
        private static int CalculateFundingPeriodYearFromDate(Date date)
        {
            return date.Month == CalendarMonth.January.Id ? date.Year - 1 : date.Year;
        }
    }
}