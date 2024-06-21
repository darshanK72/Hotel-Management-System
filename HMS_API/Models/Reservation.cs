using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Code { get; set; }

        [Required]
        public int NumberOfAdults { get; set; }

        [Required]
        public int NumberOfChildren { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int NumberOfNights { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Status { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; set; }
        public Payment? Payment { get; set; }

        [ForeignKey("Bill")]
        public int BillId { get; set; }
        public Bill? Bill { get; set; }

    }

}
