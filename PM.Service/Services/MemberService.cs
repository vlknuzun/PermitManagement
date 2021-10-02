using PM.Data.Context;
using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM.Service.Services
{
    public class MemberService : IMemberService
    {
        PermitManagementContext _dbContext = new PermitManagementContext();

        List<Member> IMemberService.GetMembers()
        {
            return _dbContext.Members.ToList();
        }
    }
}
