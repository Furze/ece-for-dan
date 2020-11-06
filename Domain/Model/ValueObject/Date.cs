using System;

namespace MoE.ECE.Domain.Model.ValueObject
{
    public class Date : IComparable<Date>
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

        private bool Equals(Date other)
        {
            return Day == other.Day && Month == other.Month && Year == other.Year;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Date) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Month, Year);
        }

        private static int ConcatDate(Date? date)
        {
            if (date is null)
            {
                return 0;
            }

            var d = int.Parse($"{date.Year}{date.Month:#00}{date.Day:#00}");
            return d;
        }

        private static int CompareTo(Date? a, Date? b)
        {
            return ConcatDate(a).CompareTo(ConcatDate(b));
        }

        public int CompareTo(Date? other)
        {
            return CompareTo(this, other);
        }

        public static bool operator !=(Date? a, Date? b)
        {
            return CompareTo(a, b) != 0;
        }

        public static bool operator ==(Date? a, Date? b)
        {
            return CompareTo(a, b) == 0;
        }

        public static bool operator >(Date? a, Date? b)
        {
            return CompareTo(a, b) == 1;
        }

        public static bool operator <(Date? a, Date? b)
        {
            return CompareTo(a, b) == -1;
        }

        public static bool operator >=(Date? a, Date? b)
        {
            return CompareTo(a, b) >= 0;
        }

        public static bool operator <=(Date? a, Date? b)
        {
            return CompareTo(a, b) <= 0;
        }
    }
}