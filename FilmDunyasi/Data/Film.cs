using Microsoft.EntityFrameworkCore;

namespace FilmDunyasi.Data
{
    public class Film
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public int Yil { get; set; }
        [Precision (3,1)]
        public decimal Puan { get; set; }

        public List<Tur> Turler { get; set; } = new();
    }
}
