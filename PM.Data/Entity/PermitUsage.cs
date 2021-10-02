using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PM.Data.Entity
{
    public class PermitUsage : BaseEntity
    {
        public PermitUsage()
        {
            this.Members = new List<Member>();
        }

        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int TitleTypeId { get; set; }
        public DateTime LeavingStartDate { get; set; }
        public DateTime LeavingEndDate { get; set; }
        public List<Member> Members { get; set; }
    }
    
}
