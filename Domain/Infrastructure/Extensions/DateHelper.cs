using System;
using System.Linq;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class DateHelper
    {
        private static TimeZoneInfo? _nzTimeZone;

        public static TimeZoneInfo GetNzTimeZone()
        {
            if (_nzTimeZone != null) return _nzTimeZone;
            // Need to check both IANA and Microsoft timezones for Windows/Linux compatibility        
            _nzTimeZone = TimeZoneInfo
                .GetSystemTimeZones()
                .SingleOrDefault(info => info.Id == "Pacific/Auckland" || info.Id == "New Zealand Standard Time");

            if (_nzTimeZone == null)
                throw new ECEApplicationException("Cannot find NZ Time Zone on the server.");

            return _nzTimeZone;
        }

        public static DateTimeOffset ToNzDateTimeOffSet(this DateTime dateTime)
        {
            var nzDateTimeOffset = new DateTimeOffset(dateTime, GetNzTimeZone().GetUtcOffset(dateTime));
         
            return nzDateTimeOffset;
        }

        public static DateTimeOffset? ToNzDateTimeOffSet(this DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset.HasValue
                ? (DateTimeOffset?)TimeZoneInfo.ConvertTime(dateTimeOffset.Value, GetNzTimeZone())
                : null;
        }
        
        public static DateTimeOffset ToNzDateTimeOffSet(this DateTimeOffset dateTimeOffset)
        {
            return TimeZoneInfo.ConvertTime(dateTimeOffset, GetNzTimeZone());
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