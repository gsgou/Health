using System;
using Foundation;

namespace Shiny.Health;

public static class AppleExtensions
{
    readonly static DateTime reference = new(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

    public static DateTimeOffset ToDateTimeOffset(this NSDate date, NSTimeZone? timeZone = null)
    {
        // Developers should be storing time zone in the samples metadata using the HKMetadataKeyTimeZone key.
        // Not many developers are taking advantage of this key or the metadata property in general.
        // Even Apple fails to add the time zone metadata to samples generated through their own Health app.

        TimeSpan offset = timeZone != null
            ? TimeSpan.FromSeconds(timeZone.GetSecondsFromGMT)
            : TimeZoneInfo.Local.BaseUtcOffset; // Assign the local time zone as fallback

        var utcDateTime = reference.AddSeconds(date.SecondsSinceReferenceDate);

        var dateTimeOffset = new DateTimeOffset(utcDateTime).ToOffset(offset);

        return dateTimeOffset;
    }
}