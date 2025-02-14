﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS_API.Models
{
    public class Guest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GuestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Gender { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? MemberCode { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }


    }
}
