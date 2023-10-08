using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Weather.BLL.Interfaces;
using Weather.DAL.Interfaces;
using Weather.DAL.Models;
using Weather.Extensions;

namespace Weather.BLL.Services
{
    /// <summary>
    /// Сервис для обработки погодных данных.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса WeatherService.
        /// </summary>
        /// <param name="weatherRepository">Репозиторий погодных данных.</param>
        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        /// <summary>
        /// Загружает погодные данные из файлов Excel и сохраняет их в базу данных.
        /// </summary>
        /// <param name="fileStreams">Список потоков файлов Excel.</param>
        /// <returns>True, если загрузка прошла успешно, в противном случае - false.</returns>
        public async Task<bool> UploadWeatherData(List<Stream> fileStreams)
        {
            try
            {
                List<WeatherRecord> records = new List<WeatherRecord>();

                foreach (var stream in fileStreams)
                {
                    using (var package = new XSSFWorkbook(stream))
                    {
                        for (int tabIndex = 0; tabIndex < package.NumberOfSheets; tabIndex++)
                        {
                            ISheet sheet = package.GetSheetAt(tabIndex);
                            for (int row = 4; row <= sheet.LastRowNum; row++)
                            {
                                var currentRow = sheet.GetRow(row);
                                if (currentRow != null)
                                {
                                    var record = new WeatherRecord
                                    {
                                        Date = currentRow.GetCell(0).GetDateValue(),
                                        Time = currentRow.GetCell(1).GetTimeValue(),
                                        Temperature = currentRow.GetCell(2).GetNumericValue(),
                                        RelativeHumidity = currentRow.GetCell(3).GetNumericValue(),
                                        DewPoint = currentRow.GetCell(4).GetNumericValue(),
                                        AtmosphericPressure = currentRow.GetCell(5).GetNumericValue(),
                                        WindDirection = currentRow.GetCell(6).GetStringCellValue(),
                                        WindSpeed = currentRow.GetCell(7).GetNumericValue(),
                                        Cloudiness = currentRow.GetCell(8).GetNumericValue(),
                                        CloudBaseHeight = currentRow.GetCell(9).GetNumericValue(),
                                        Visibility = currentRow.GetCell(10).GetNumericValue(),
                                        WeatherPhenomena = currentRow.GetCell(11).GetStringCellValue(),
                                    };

                                    records.Add(record);
                                }
                            }
                        }
                    }
                }

                // Сохранение записей в базу данных
                foreach (var record in records)
                {
                    await _weatherRepository.Create(record);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование
                return false;
            }
        }

        /// <summary>
        /// Фильтрует погодные данные по году и месяцу с использованием пагинации.
        /// </summary>
        /// <param name="year">Год для фильтрации.</param>
        /// <param name="month">Месяц для фильтрации.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество записей на странице.</param>
        /// <returns>Список погодных записей, удовлетворяющих условиям фильтрации, и общее количество записей.</returns>
        public async Task<(List<WeatherRecord> records, int totalRecords)> FilterWeatherDataByYearAndMonth(int year, int month, int page, int pageSize)
        {
            try
            {
                var filteredRecords = await _weatherRepository.FilterByYearAndMonth(year, month, page, pageSize);
                return filteredRecords;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование
                throw;
            }
        }

    }
}
