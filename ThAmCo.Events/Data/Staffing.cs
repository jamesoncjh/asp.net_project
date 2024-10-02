using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staffing
    {
        [Key]
        public int StaffId { get; set; }

        public Staff Staff { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public bool Attended { get; set; }
    }
}

