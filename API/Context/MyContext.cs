using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder obj)
        {
            if (!obj.IsConfigured)
            {
                obj.UseLazyLoadingProxies();
            }
        }

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.University> Universities { get; set; }
        public DbSet<Models.Education> Educations { get; set; }
        public DbSet<Models.Profiling> Profilings { get; set; }
        public DbSet<Models.Account> Accounts { get; set; }
        public DbSet<Models.AccountRole> AccountRoles { get; set; }
        public DbSet<Models.Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
               .HasOne(a => a.Profiling)
               .WithOne(p => p.Account)
               .HasForeignKey<Profiling>(p => p.NIK);

            modelBuilder.Entity<Profiling>()
                .HasOne(p => p.Education)
                .WithMany(e => e.Profilings)
                .HasForeignKey(p => p.Education_Id);

            modelBuilder.Entity<Education>()
                .HasOne(e => e.University)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.University_Id);

            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.Account_NIK, ar.Role_Id });

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRole)
                .HasForeignKey(ar => ar.Account_NIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRole)
                .HasForeignKey(ar => ar.Role_Id);
        }
    }
}
