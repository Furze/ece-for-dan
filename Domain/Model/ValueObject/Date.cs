using System;

namespace MoE.ECE.Domain.Model.ValueObject
{
    public class Date : IComparable<Date>, IEquatable<Date>
    {
        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public int Day { get; }

        public int Month { get; }
        public int Year { get; }

        public int CompareTo(Date? other) => CompareTo(this, other);

        public bool Equals(Date? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Day == other.Day && Month == other.Month && Year == other.Year;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Date)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Day, Month, Year);

        private static int ConcatDate(Date? date)
        {
            if (date is null)
            {
                return 0;
            }

            int d = int.Parse($"{date.Year}{date.Month:#00}{date.Day:#00}");
            return d;
        }

        private static int CompareTo(Date? a, Date? b) => ConcatDate(a).CompareTo(ConcatDate(b));

        public static bool operator !=(Date? a, Date? b) => CompareTo(a, b) != 0;

        public static bool operator ==(Date? a, Date? b) => CompareTo(a, b) == 0;

        public static bool operator >(Date? a, Date? b) => CompareTo(a, b) == 1;

        public static bool operator <(Date? a, Date? b) => CompareTo(a, b) == -1;

        public static bool operator >=(Date? a, Date? b) => CompareTo(a, b) >= 0;

        public static bool operator <=(Date? a, Date? b) => CompareTo(a, b) <= 0;
    }
}