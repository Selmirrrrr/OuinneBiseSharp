namespace Bizy.OuinneBiseSharp.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static string ToOuinneBiseString(this DateTime date) => date.ToString("yyyy-MM-dd");
    }
}
