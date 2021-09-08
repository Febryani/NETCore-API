using Microsoft.EntityFrameworkCore;
using NETCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Context
{
    public class MyContext : DbContext //gateway app dengan database
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Person)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profiling>(b => b.NIK);

            /*modelBuilder.Entity<Role>()
                .HasMany(a => a.AccountRoles)
                .WithOne(b => b.Role);*/

            modelBuilder.Entity<AccountRole>()
               .HasOne<Role>(a => a.Role)
               .WithMany(b => b.AccountRoles)
               .HasForeignKey(a => a.RoleId);

            modelBuilder.Entity<AccountRole>()
                .HasOne<Account>(a => a.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(a => a.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(a => a.Profilings)
                .WithOne(b => b.Education);

            modelBuilder.Entity<University>()
                .HasMany(a => a.Educations)
                .WithOne(b => b.University);
        }
    }
}
