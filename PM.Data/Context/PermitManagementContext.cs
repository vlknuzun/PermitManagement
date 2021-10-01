using Microsoft.EntityFrameworkCore;
using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Data.Context
{
    public class PermitManagementContext : DbContext
    {
        public PermitManagementContext(DbContextOptions<PermitManagementContext> options) : base(options) { }
        public DbSet<PermitUsage> PermitUsages { get; set; }
        public DbSet<TitleType> TitleTypes { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
