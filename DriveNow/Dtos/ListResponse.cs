using DriveNow.Models;

namespace DriveNow.Dtos
{
    public class ListResponse<T>
    {
        public List<T> elements { get; set; } = new List<T>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int elementsCount { get; set; }  
    }
}
