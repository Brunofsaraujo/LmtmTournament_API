using System;

namespace LmtmTournament.Infra.Data.Odbc.Extensions
{
    public static class DatetimeExtensions
    {
        public static string DatetimeToString(this DateTime datetime) =>
            datetime.ToString("yyy-MM-dd HH:MM");
    }
}
