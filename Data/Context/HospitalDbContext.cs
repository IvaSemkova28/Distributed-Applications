using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Context
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Iva\Documents\HospitalDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True", b => b.MigrationsAssembly("Data"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
        }
        
    }
}
