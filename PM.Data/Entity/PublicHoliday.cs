using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PM.Data.Entity
{
    public class PublicHoliday
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public int Day { get; set; }
    }
}
