using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class StaffRole
    {
        public int Id { get; set; }

        [Required]
        public string Role { get; set; }

        public int StaffId { get; set; }
        //public Staff Staff { get; set; }
    }
}
