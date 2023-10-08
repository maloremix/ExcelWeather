using Weather.DAL.Models;

namespace Weather.Models
{
    public class PaginationViewModel
    {
        public List<WeatherRecord> Records { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get { return Page > 1; }
        }

        public bool HasNextPage
        {
            get { return Page < TotalPages; }
        }
    }

}
