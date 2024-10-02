using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodList
    {
        [Key]
        public int FoodListId{ get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public string MenuId { get; set; }

        [Range(0.0, Double.MaxValue)]
        public double Price { get; set; }
        public Menu Menu { get; set; }
    }
}
