using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.DAL.Interfaces;
using Weather.DAL.Models;

namespace Weather.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для работы с погодными записями в базе данных.
    /// </summary>
    public class WeatherRepository : IWeatherRepository
    {
        private readonly ApplicationDbContext _db;

        public WeatherRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Создает новую погодную запись в базе данных.
        /// </summary>
        /// <param name="entity">Погодная запись для создания.</param>
        /// <returns>True, если операция выполнена успешно, в противном случае - false.</returns>
        public async Task<bool> Create(WeatherRecord entity)
        {
            _db.WeatherRecords.Add(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удаляет погодную запись из базы данных.
        /// </summary>
        /// <param name="entity">Погодная запись для удаления.</param>
        /// <returns>True, если операция выполнена успешно, в противном случае - false.</returns>
        public async Task<bool> Delete(WeatherRecord entity)
        {
            _db.WeatherRecords.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Получает погодную запись по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор погодной записи.</param>
        /// <returns>Объект WeatherRecord или null, если погодная запись не найдена.</returns>
        public async Task<WeatherRecord> Get(int id)
        {
            return await _db.WeatherRecords.FirstOrDefaultAsync(x => x.ID == id);
        }

        /// <summary>
        /// Получает список всех погодных записей в базе данных.
        /// </summary>
        /// <returns>Список погодных записей.</returns>
        public async Task<List<WeatherRecord>> Select()
        {
            return await _db.WeatherRecords.ToListAsync();
        }

        /// <summary>
        /// Обновляет информацию о погодной записи в базе данных.
        /// </summary>
        /// <param name="entity">Погодная запись для обновления.</param>
        /// <returns>Обновленный объект WeatherRecord.</returns>
        public async Task<WeatherRecord> Update(WeatherRecord entity)
        {
            _db.WeatherRecords.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Фильтрует погодные записи по указанному году и месяцу и возвращает их с пагинацией.
        /// </summary>
        /// <param name="year">Год для фильтрации.</param>
        /// <param name="month">Месяц для фильтрации.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество записей на странице.</param>
        /// <returns>Список погодных записей, соответствующих заданным условиям, и общее количество записей.</returns>
        public async Task<(List<WeatherRecord> records, int totalRecords)> FilterByYearAndMonth(int year, int month, int page, int pageSize)
        {
            // Фильтрация записей по году и месяцу
            var filteredRecords = _db.WeatherRecords
                .Where(record => record.Date.HasValue && record.Date.Value.Year == year && record.Date.Value.Month == month);

            // Вычисление общего количества записей
            var totalRecords = await filteredRecords.CountAsync();

            // Выборка данных с пагинацией
            var paginatedRecords = await filteredRecords
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Преобразование дат из UTC в локальное время
            paginatedRecords.ForEach(record =>
            {
                if (record.Date.HasValue)
                {
                    record.Date = record.Date.Value.ToLocalTime();
                }
            });

            return (paginatedRecords, totalRecords);
        }



    }
}
