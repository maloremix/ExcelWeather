using Automat.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.DAL.Models;

namespace Weather.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с погодными записями в базе данных.
    /// </summary>
    public interface IWeatherRepository : IBaseRepository<WeatherRecord>
    {
        /// <summary>
        /// Фильтрует погодные записи по указанному году и месяцу с пагинацией.
        /// </summary>
        /// <param name="year">Год для фильтрации.</param>
        /// <param name="month">Месяц для фильтрации.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество записей на странице.</param>
        /// <returns>Список погодных записей, соответствующих заданным условиям, и общее количество записей.</returns>
        Task<(List<WeatherRecord> records, int totalRecords)> FilterByYearAndMonth(int year, int month, int page, int pageSize);
    }
}
