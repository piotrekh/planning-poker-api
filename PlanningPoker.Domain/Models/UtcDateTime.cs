using System;

namespace PlanningPoker.Domain.Models
{
    /// <summary>
    /// This model is created specifically to resolve problems with DateTime conversion
    /// and timezones when sending DateTimes as strings to ASP.NET (time is converted
    /// to local server time but timezone is then unknown), so we'll use it across
    /// ASP.NET requests and responses
    /// </summary>
    public class UtcDateTime
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public UtcDateTime() { }

        public UtcDateTime(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            Month = dateTime.Month;
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
        }
    }
}
