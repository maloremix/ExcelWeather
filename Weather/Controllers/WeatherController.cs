using Microsoft.AspNetCore.Mvc;
using Weather.BLL.Interfaces;
using Weather.DAL.Models;
using Weather.Models;

namespace Weather.Controllers
{
    /// <summary>
    /// Контроллер для управления погодными данными.
    /// </summary>
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера WeatherController.
        /// </summary>
        /// <param name="weatherService">Сервис для работы с погодными данными.</param>
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Главная страница.
        /// </summary>
        /// <returns>Представление главной страницы.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Страница загрузки архивов погодных условий.
        /// </summary>
        /// <returns>Представление страницы загрузки.</returns>
        public IActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Действие для обработки загрузки файлов Excel.
        /// </summary>
        /// <param name="files">Список файлов для загрузки.</param>
        /// <returns>Редирект на главную страницу.</returns>
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                List<Stream> fileStreams = new List<Stream>();

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var stream = file.OpenReadStream();
                        fileStreams.Add(stream);
                    }
                }

                // Вызываем сервис для обработки загруженных файлов Excel
                bool result = await _weatherService.UploadWeatherData(fileStreams);
                if (!result)
                {
                    // Обработка ошибок загрузки
                    ModelState.AddModelError("", "Ошибка загрузки файла.");
                }
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Страница просмотра архивов погодных условий.
        /// </summary>
        /// <returns>Представление страницы просмотра погодных данных.</returns>
        public IActionResult ViewWeather()
        {
            return View();
        }

        /// <summary>
        /// Действие для фильтрации записей о погоде по месяцам и годам.
        /// </summary>
        /// <param name="year">Год для фильтрации.</param>
        /// <param name="month">Месяц для фильтрации.</param>
        /// <param name="page">Номер страницы.</param>
        /// <returns>Представление страницы просмотра погодных данных с примененными фильтрами.</returns>
        public async Task<IActionResult> FilterWeatherData(int year, int month, int page = 1)
        {
            var pageSize = 10; // Количество записей на странице

            var (filteredRecords, totalRecordsCount) = await _weatherService.FilterWeatherDataByYearAndMonth(year, month, page, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalRecordsCount / pageSize); // Вычисляем общее количество страниц


            var viewModel = new PaginationViewModel
            {
                Records = filteredRecords,
                Year = year,
                Month = month,
                Page = page,
                TotalPages = totalPages
            };

            return View("ViewWeather", viewModel);
        }

    }
}
