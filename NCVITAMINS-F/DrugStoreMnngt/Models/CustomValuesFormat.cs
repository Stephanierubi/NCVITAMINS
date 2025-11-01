using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugStoreMnngt.Models
{
    public static class CustomValuesFormat
    {
        public static DateTime currentDateTimeUTC()
        {
            String utcTimeZone = System.Configuration.ConfigurationManager.AppSettings["timeZone"];
            DateTime databaseUtcTime = DateTime.UtcNow;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(utcTimeZone);
            var timeZoneout = TimeZoneInfo.ConvertTimeFromUtc(databaseUtcTime, timeZoneInfo);

            return timeZoneout;
        }
    }
}