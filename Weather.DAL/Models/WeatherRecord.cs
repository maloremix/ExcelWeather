using System;

namespace Weather.DAL.Models
{
    /// <summary>
    /// Представляет запись о погодных условиях в базе данных.
    /// </summary>
    public class WeatherRecord
    {
        /// <summary>
        /// Уникальный идентификатор записи о погоде.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Дата погодных измерений.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Время погодных измерений.
        /// </summary>
        public TimeSpan? Time { get; set; }

        /// <summary>
        /// Температура в градусах Цельсия.
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// Относительная влажность.
        /// </summary>
        public double? RelativeHumidity { get; set; }

        /// <summary>
        /// Точка росы (Td) в градусах Цельсия.
        /// </summary>
        public double? DewPoint { get; set; }

        /// <summary>
        /// Атмосферное давление в миллиметрах ртути.
        /// </summary>
        public double? AtmosphericPressure { get; set; }

        /// <summary>
        /// Направление ветра.
        /// </summary>
        public string? WindDirection { get; set; }

        /// <summary>
        /// Скорость ветра в м/с.
        /// </summary>
        public double? WindSpeed { get; set; }

        /// <summary>
        /// Облачность в процентах.
        /// </summary>
        public double? Cloudiness { get; set; }

        /// <summary>
        /// Нижняя граница облачности в метрах.
        /// </summary>
        public double? CloudBaseHeight { get; set; }

        /// <summary>
        /// Горизонтальная видимость в километрах.
        /// </summary>
        public double? Visibility { get; set; }

        /// <summary>
        /// Погодные явления (например, дождь, снег, ясно).
        /// </summary>
        public string? WeatherPhenomena { get; set; }
    }
}
