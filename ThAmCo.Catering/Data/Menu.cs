using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        [Key]
        public string MenuId { get; set; }
        public string MenuName { get; set; }

        public string FoodBookingId { get; set; }
        //Reference FoodBooking.cs and store retrieve values in FoodBookingList
        public List<FoodBooking> FoodBookingList { get; set; }
        public List<FoodList> FoodList { get; set; }
    }
}
