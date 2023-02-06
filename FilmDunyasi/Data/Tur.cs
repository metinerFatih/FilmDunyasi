using System.ComponentModel.DataAnnotations;

namespace FilmDunyasi.Data
{
    public class Tur
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Ad { get; set; } = null!;

        public List<Film> Filmler { get; set; } = new();
    }
}
