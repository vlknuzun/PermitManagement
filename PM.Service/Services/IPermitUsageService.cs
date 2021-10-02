using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.Services
{
    public interface IPermitUsageService
    {
        public List<PermitUsage> AddPermitUsage(PermitUsage permitUsage);

        public List<PermitUsage> GetPermitUsages();
    }
}
