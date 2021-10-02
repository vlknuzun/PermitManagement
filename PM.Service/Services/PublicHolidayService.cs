using PM.Data.Context;
using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.Service.Services
{
    public class PublicHolidayService : IPublicHolidayService
    {
        PermitManagementContext _dbContext = new PermitManagementContext();

        public List<PublicHoliday> GetPublicHolidays()
        {
            return _dbContext.PublicHolidays.ToList();
        }

    }
}
