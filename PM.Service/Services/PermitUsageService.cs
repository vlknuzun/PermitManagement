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
        
        public void GetSomeValues()
        {
            var values = _dbContext.Members.FirstOrDefault();
        }

    }
}
