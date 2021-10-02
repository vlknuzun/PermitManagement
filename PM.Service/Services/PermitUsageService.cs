using PM.Data.Context;
using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.Service.Services
{
    public class PermitUsageService : IPermitUsageService
    {
        PermitManagementContext _dbContext = new PermitManagementContext();

        List<PermitUsage> IPermitUsageService.AddPermitUsage(PermitUsage permitUsage)
        {
            _dbContext.PermitUsages.Add(permitUsage);
            return _dbContext.PermitUsages.ToList();
        }

        List<PermitUsage> IPermitUsageService.GetPermitUsages()
        {
            return  _dbContext.PermitUsages.ToList();
        }
    }
}
