using System;
using System.Linq;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class DateHelper
    {
        private static TimeZoneInfo? _nzTimeZone;

        public static TimeZoneInfo NzTimeZone
        {
            get
            {
                if (_nzTimeZone != null) return _nzTimeZone;
                // Need to check both IANA and Microsoft timezones for Windows/Linux compatibility        
                _nzTimeZone = TimeZoneInfo
                    .GetSystemTimeZones()
                    .SingleOrDefault(info => info.Id == "Pacific/Auckland" || info.Id == "New Zealand Standard Time");

                if (_nzTimeZone == null)
                    throw new Exception("Cannot find NZ Time Zone on the server.");

                return _nzTimeZone;
            }
        }

        public static DateTimeOffset ToNzDateTimeOffSet(this DateTime dateTime)
        {
            var nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), NzTimeZone);

            var nzDateTimeOffset = new DateTimeOffset(nzDateTime, NzTimeZone.BaseUtcOffset);

            return nzDateTimeOffset;
        }

        public static DateTimeOffset ToNzDateTimeOffSet(this DateTimeOffset dateTimeOffset)
        {
            return TimeZoneInfo.ConvertTime(dateTimeOffset, NzTimeZone);
        }

        public static Date ToNzDate(this DateTimeOffset dateTime)
        {
            var nzDateTimeOffset = ToNzDateTimeOffSet(dateTime);

            return new Date(nzDateTimeOffset.Day, nzDateTimeOffset.Month, nzDateTimeOffset.Year);
        }

        public static Date ToNzDate(this DateTime dateTime)
        {
            var nzDateTimeOffset = ToNzDateTimeOffSet(dateTime);

            return new Date(nzDateTimeOffset.Day, nzDateTimeOffset.Month, nzDateTimeOffset.Year);
        }
    }
}