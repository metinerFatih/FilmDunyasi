using FilmDunyasi.Data;

namespace FilmDunyasi.Models
{
    public class HomeViewModel
    {
        public List<Film> Filmler { get; set; } = new();

        public int? TurId { get; set; }
    }
}
