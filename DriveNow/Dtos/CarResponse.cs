using DriveNow.Models;

namespace DriveNow.Dtos
{
    public class CarResponse
    {
        public List<Car> Cars { get; set; } = new List<Car>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int CarsCount { get; set; }  
    }
}
