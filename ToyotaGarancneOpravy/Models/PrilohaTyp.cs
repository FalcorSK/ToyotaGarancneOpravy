using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToyotaGarancneOpravy.Models
{
    [Table("PrilohaTyp")]
    public class PrilohaTyp
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrilohaTypId { get; set; }

        [MaxLength(100)]
        public string PrilohaNazov { get; set; } = null!;

        [MaxLength(100)]
        public string? PrilohaPopis { get; set; }
        public int? Aktivna { get; set; }
        public DateTime? CreatedDate { get; set; }

        [MaxLength(100)]
        public string? CreateddBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        public virtual ICollection<Priloha>? Prilohy { get; set; }
    }
}
