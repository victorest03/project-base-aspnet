using System;

namespace Common.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ConverToTimeZone(this DateTime datetime, string utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, TimeZoneInfo.FindSystemTimeZoneById(utc));
        }
    }
}
