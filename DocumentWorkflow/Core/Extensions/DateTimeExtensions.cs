using System.Globalization;

namespace DocumentWorkflow.Core.Extensions
{
    static class DateTimeExtensions
    {
        private static readonly List<string> _monthsGenitiveCaseName = new List<string>
        {
            "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря"
        };

        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToMonthGenitiveCaseName(this DateTime dateTime)
        {
            return _monthsGenitiveCaseName[dateTime.Month - 1];
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
    }
}
