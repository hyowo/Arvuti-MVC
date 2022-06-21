using System.ComponentModel.DataAnnotations;

namespace Arvuti_MVC.Models
{
    public class TellimusModel
    {
        public int ID { get; set; }
        [Required]
        public string? Nimi { get; set; }
        [Required]
        public string? Arvuti { get; set; }
        [Required]
        public string? Monitor { get; set; }
        [Required]
        public string? Klaviatuur { get; set; }
        [Required]
        public string? Hiir { get; set; }
        [MaxLength(64)]
        public string? Lisainfo { get; set; }
        
        public bool AruvtiOlemas { get; set; }
        public bool MonitorOlemas { get; set; }
        public bool KlaviatuurOlemas { get; set; }
        public bool HiirOlemas { get; set; }
        public bool Pakitud { get; set; }
        public bool ValjaSaadetud { get; set; }
    }
}
