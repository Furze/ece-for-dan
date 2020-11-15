namespace MoE.ECE.Domain.Model.ValueObject
{
    public struct MonthYear
    {
        public CalendarMonth Month { get; }

        public int Year { get; }

        public MonthYear(CalendarMonth month, int year)
        {
            Month = month;
            Year = year;
        }
    }
}