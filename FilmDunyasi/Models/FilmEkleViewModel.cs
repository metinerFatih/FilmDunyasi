using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FilmDunyasi.Models
{
    public class FilmEkleViewModel
    {
        public string Ad { get; set; } = null!;
        public int Yil { get; set; }
        [Required]
        public decimal? Puan { get; set; } //string girildiğinde veya boş girildiğinde boş bırakılamaz ya da farklı bir tür girildi hatalarını ayrı ayrı almak için ? ve [required] girdik..
        public int[] Turler { get; set; } = Array.Empty<int>();
    }
}
