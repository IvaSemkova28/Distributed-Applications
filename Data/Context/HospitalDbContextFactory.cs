using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
    {
        public HospitalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            object value = optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Iva\Documents\HospitalDb.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}
