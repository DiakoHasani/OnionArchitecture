using System.Globalization;
namespace OA.Common.Helpers;

public static class DateHelper
{
    public static string GetShamsiDate(this DateTime date)
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
    }
}