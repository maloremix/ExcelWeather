using Weather.DAL.Models;

namespace Weather.Models
{
    /// <summary>
    /// Модель представления для пагинации погодных записей.
    /// </summary>
    public class PaginationViewModel
    {
        /// <summary>
        /// Список погодных записей на текущей странице.
        /// </summary>
        public List<WeatherRecord> Records { get; set; }

        /// <summary>
        /// Год, по которому производится фильтрация.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Месяц, по которому производится фильтрация.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Показывает, есть ли предыдущая страница.
        /// </summary>
        public bool HasPreviousPage
        {
            get { return Page > 1; }
        }

        /// <summary>
        /// Показывает, есть ли следующая страница.
        /// </summary>
        public bool HasNextPage
        {
            get { return Page < TotalPages; }
        }
    }


}
