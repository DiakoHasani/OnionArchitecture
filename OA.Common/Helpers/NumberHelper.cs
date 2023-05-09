namespace OA.Common.Helpers;
public static class NumbrtHelper
{
    public static string GetEnglishNumber(this string number)
    {
        return number.Replace('۰', '0')
        .Replace('۱','1')
        .Replace('۲','2')
        .Replace('۳','3')
        .Replace('۴','4')
        .Replace('۵','5')
        .Replace('۶','6')
        .Replace('۷','7')
        .Replace('۸','8')
        .Replace('۹','9');
    }
}