using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Weather.DAL.Models;

namespace Weather.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с погодными данными.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Загружает данные о погоде из файлов Excel.
        /// </summary>
        /// <param name="fileStreams">Список потоков файлов Excel.</param>
        /// <returns>True, если загрузка выполнена успешно, в противном случае - false.</returns>
        Task<bool> UploadWeatherData(List<Stream> fileStreams);

        /// <summary>
        /// Фильтрует погодные данные по году и месяцу с использованием пагинации.
        /// </summary>
        /// <param name="year">Год для фильтрации.</param>
        /// <param name="month">Месяц для фильтрации.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество записей на странице.</param>
        /// <returns>Список погодных записей, удовлетворяющих условиям фильтрации, и общее количество записей.</returns>
        Task<(List<WeatherRecord> records, int totalRecords)> FilterWeatherDataByYearAndMonth(int year, int month, int page, int pageSize);
    }
}
