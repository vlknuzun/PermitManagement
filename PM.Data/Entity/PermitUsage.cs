using PM.Core;
using PM.Core.Repository.ConreateRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Data.Entity
{
    public class PermitUsage : PermitUsageRepository
    {
        public PermitUsage()
        {
            this.Members = new List<Member>();
        }
        public int MemberId { get; set; }
        public DateTime LeavingStartDate { get; set; }
        public DateTime LeavingEndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Member> Members { get; set; }
    }
    
}
