using Weather.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Weather.DAL
{
    /// <summary>
    /// Контекст базы данных для работы с погодными записями.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для каждой сущности (таблицы) в базе данных
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
    }
}
