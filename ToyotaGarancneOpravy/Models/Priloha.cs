using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyotaGarancneOpravy.Models
{
    [Table("Priloha")]
    public class Priloha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrilohaId { get; set; }
        
        [ForeignKey("GarancnaOpravaId")]
        public int GarancnaOpravaId { get; set; }
        public virtual GarancnaOprava garancnaOprava { get; set; } = null!;

        public int? PrilohaTyp { get; set; }

        [MaxLength(100)]
        public string? PrilohaNazov { get; set; }

        [MaxLength]
        public byte[]? PrilohaSubor { get; set; }

        [MaxLength(100)]
        public string? PrilohaFileName { get; set; }

        public DateTime? CreateDate { get; set; }

        [MaxLength(100)]
        public string? CreateBy { get; set; }
        
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(100)]
        public string? ModifiiedBy { get; set; }


        [NotMapped]
        public IFormFile? PrilohaFile { get; set; } = null!;

        //public virtual ICollection<Priloha> Prilohy { get; set; } = null!;
        public virtual PrilohaTyp? PrilohaTypNavigation { get; set; }


    }
}
