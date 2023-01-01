using DriveNow.Models;

namespace DriveNow.Dtos
{
    public class ListResponse
    {
        public List<Car> elements { get; set; } = new List<Car>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int elementsCount { get; set; }  
    }
}
