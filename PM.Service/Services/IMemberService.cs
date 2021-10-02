using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.Services
{
    public interface IMemberService
    {
        public List<Member> GetMembers();
    }
}
