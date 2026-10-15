
using System;
using System.ComponentModel.DataAnnotations;

namespace SalonBeauty
{
    public class SalonBooking
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string ServiceName { get; set; } = string.Empty;

        [Required]
        public DateTime? VisitDate { get; set; }

        [Range(typeof(decimal), "0.01", "50000")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Новая";
    }
}
