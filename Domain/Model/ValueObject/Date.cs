using System;
using System.Collections.Generic;

namespace MoE.ECE.Domain.Model.ValueObject
{
    public class Date : IComparable<Date>, IEquatable<Date>, IEqualityComparer<Date>
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

        public int CompareTo(Date? other)
        {
            return CompareTo(this, other);
        }

        public bool Equals(Date? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Day == other.Day && Month == other.Month && Year == other.Year;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Date) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Month, Year);
        }

        private static int ConcatDate(Date? date)
        {
            if (date is null) return 0;

            var d = int.Parse($"{date.Year}{date.Month:#00}{date.Day:#00}");
            return d;
        }

        private static int CompareTo(Date? a, Date? b)
        {
            return ConcatDate(a).CompareTo(ConcatDate(b));
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

        public bool Equals(Date? x, Date? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Day == y.Day && x.Month == y.Month && x.Year == y.Year;
        }

        public int GetHashCode(Date obj)
        {
            return HashCode.Combine(obj.Day, obj.Month, obj.Year);
        }
    }
}