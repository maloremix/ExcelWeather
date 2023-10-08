using NPOI.SS.UserModel;

namespace Weather.Extensions
{
    /// <summary>
    /// Методы расширения для интерфейса ICell из библиотеки NPOI.SS.UserModel.
    /// </summary>
    public static class CellExtensions
    {
        /// <summary>
        /// Получает значение ячейки в виде даты и времени.
        /// </summary>
        /// <param name="cell">Ячейка.</param>
        /// <returns>Значение даты и времени или null, если ячейка пуста или не содержит корректного значения.</returns>
        public static DateTime? GetDateValue(this ICell cell)
        {
            if (cell != null && !string.IsNullOrEmpty(cell.StringCellValue))
            {
                var parsedDateTime = DateTime.ParseExact(cell.StringCellValue, "dd.MM.yyyy", null);
                return TimeZoneInfo.ConvertTimeToUtc(parsedDateTime, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
            }
            return null;
        }

        /// <summary>
        /// Получает значение ячейки в виде времени.
        /// </summary>
        /// <param name="cell">Ячейка.</param>
        /// <returns>Значение времени или null, если ячейка пуста или не содержит корректного значения.</returns>
        public static TimeSpan? GetTimeValue(this ICell cell)
        {
            if (cell != null && !string.IsNullOrEmpty(cell.StringCellValue))
            {
                return TimeSpan.ParseExact(cell.StringCellValue, "hh\\:mm", null);
            }
            return null;
        }

        /// <summary>
        /// Получает значение ячейки в виде числа.
        /// </summary>
        /// <param name="cell">Ячейка.</param>
        /// <returns>Значение числа или null, если ячейка пуста или не содержит числового значения.</returns>
        public static double? GetNumericValue(this ICell cell)
        {
            if (cell != null && cell.CellType == CellType.Numeric)
            {
                return cell.NumericCellValue;
            }
            return null;
        }

        /// <summary>
        /// Получает значение ячейки в виде строки.
        /// </summary>
        /// <param name="cell">Ячейка.</param>
        /// <returns>Значение строки или null, если ячейка пуста или не содержит текстового значения.</returns>
        public static string? GetStringCellValue(this ICell cell)
        {
            if (cell != null && !string.IsNullOrEmpty(cell.StringCellValue))
            {
                return cell.StringCellValue;
            }
            return null;
        }
    }
}
