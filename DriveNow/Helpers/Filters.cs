using DriveNow.Models;

namespace DriveNow.Helpers
{
    public class Filters
    {
        public string? key{get;set;} = null;
        public int? minPrice { get; set; } = null;
        public int? maxPrice { get; set; } = null;
        public int? maxkilometrage { get; set; } = null;
        public FType? typegasoile { get; set; } = null;
    }
}
