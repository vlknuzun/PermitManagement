using Microsoft.EntityFrameworkCore;
using PM.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Data.Context
{
    public class PermitManagementContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-U18OS4K;Database=PermitManagement;Trusted_Connection=True;");
        }
        public DbSet<PermitUsage> PermitUsages { get; set; }
        public DbSet<TitleType> TitleTypes { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
