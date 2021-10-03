using PM.Data.Entity;
using PM.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.Services
{
    public interface IPermitUsageService
    {
        public void AddPermitUsage(PermitUsage permitUsage);
        public void AddPermitUsage(List<PermitUsage> permitUsages);
        public List<PermitUsage> GetPermitUsages();
        public void DistributeLeaves();
        public List<UsageLeavesViewModel> GetCurretPermits();

    }
}
