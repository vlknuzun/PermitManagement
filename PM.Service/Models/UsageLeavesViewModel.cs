using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.Models
{
    public class UsageLeavesViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime LeavingStartDate { get; set; }
        public DateTime LeavingEndDate { get; set; }
    }
}
