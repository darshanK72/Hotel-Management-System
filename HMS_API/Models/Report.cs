using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ReportType { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }

        [Required]
        public string? Data { get; set; }
    }
}
