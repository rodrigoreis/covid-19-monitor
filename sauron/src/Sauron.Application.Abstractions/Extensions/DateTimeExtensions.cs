using System;

namespace Sauron.Application.Abstractions.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset ToDateTimeOffset(this DateTime date, TimeSpan offset)
        {
            return date == DateTime.MinValue ? DateTimeOffset.MinValue : new DateTimeOffset(date.Ticks, offset);
        }

        public static DateTimeOffset ToDateTimeOffset(this DateTime date, double offsetInHours = 0d)
        {
            return ToDateTimeOffset(date, offsetInHours.Equals(0d) ? TimeSpan.Zero : TimeSpan.FromHours(offsetInHours));
        }
    }
}
