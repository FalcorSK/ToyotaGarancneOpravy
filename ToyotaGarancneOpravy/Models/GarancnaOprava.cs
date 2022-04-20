using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyotaGarancneOpravy.Models
{
    [Table("GarancnaOprava")]
    public class GarancnaOprava
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GarancnaOpravaId { get; set; }
        
        [MaxLength(100)]
        public string ZakazkaTb { get; set; } = null!;

        [MaxLength]
        public byte[]? TbScan { get; set; }

        [MaxLength(100)]
        public string? TbFileName { get; set; }

        [MaxLength(100)]
        public string ZakazkaTg { get; set; } = null!;

        [MaxLength]
        public byte[]? TgScan { get; set; }

        [MaxLength(100)]
        public string? TgFileName { get; set; }

        [MaxLength(100)]
        public string? Vin { get; set; }
        [MaxLength(100)]
        public string? Cws { get; set; }

        [MaxLength(100)]
        public string? Protokol { get; set; }
       
        public DateTime? CreatedDate { get; set; }
        
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        public virtual List<Priloha> Prilohy { get; set; } = new List<Priloha>();


        [NotMapped]
        public IFormFile? TBFile { get; set; } = null!;

        [NotMapped]
        public IFormFile? TGFile { get; set; } = null!;

    }
}
