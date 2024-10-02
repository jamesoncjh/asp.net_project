using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        [Key]
        public string FoodBookingId { get; set; }

        [Required]
        public string Date { get; set; }
        [Required]
        public string MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
