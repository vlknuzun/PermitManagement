using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.Models
{
    public class UsagesDay
    {
        public DateTime UsageDay { get; set; }
        public int UserId { get; set; }
        public int TitleTypeId { get; set; }
        public bool IsSaved { get; set; }
        public bool IsBlocked { get; set; }

    }

}
