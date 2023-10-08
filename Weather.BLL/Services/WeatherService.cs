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
                    // Загрузка данных из потока Excel
                    var package = LoadExcelPackage(stream);
                    records.AddRange(ParseWeatherRecords(package));
                }

                // Сохранение записей в базу данных
                await SaveRecordsToDatabase(records);

                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, логирование
                return false;
            }
        }

        /// <summary>
        /// Загружает данные из потока Excel и возвращает рабочую книгу.
        /// </summary>
        /// <param name="stream">Поток данных Excel.</param>
        /// <returns>Рабочая книга Excel.</returns>
        private XSSFWorkbook LoadExcelPackage(Stream stream)
        {
            using (var package = new XSSFWorkbook(stream))
            {
                return package;
            }
        }

        /// <summary>
        /// Разбирает погодные записи из рабочей книги Excel.
        /// </summary>
        /// <param name="package">Рабочая книга Excel.</param>
        /// <returns>Список погодных записей.</returns>
        private IEnumerable<WeatherRecord> ParseWeatherRecords(XSSFWorkbook package)
        {
            List<WeatherRecord> records = new List<WeatherRecord>();

            for (int tabIndex = 0; tabIndex < package.NumberOfSheets; tabIndex++)
            {
                ISheet sheet = package.GetSheetAt(tabIndex);
                for (int row = 4; row <= sheet.LastRowNum; row++)
                {
                    var currentRow = sheet.GetRow(row);
                    if (currentRow != null)
                    {
                        var record = CreateWeatherRecordFromRow(currentRow);
                        records.Add(record);
                    }
                }
            }

            return records;
        }

        /// <summary>
        /// Создает погодную запись из строки Excel.
        /// </summary>
        /// <param name="currentRow">Строка Excel.</param>
        /// <returns>Погодная запись.</returns>
        private WeatherRecord CreateWeatherRecordFromRow(IRow currentRow)
        {
            return new WeatherRecord
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
        }

        /// <summary>
        /// Сохраняет записи в базу данных.
        /// </summary>
        /// <param name="records">Список погодных записей.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        private async Task SaveRecordsToDatabase(IEnumerable<WeatherRecord> records)
        {
            foreach (var record in records)
            {
                await _weatherRepository.Create(record);
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
