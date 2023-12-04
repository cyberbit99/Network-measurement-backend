using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Network_measurement_database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_database.Repository
{
    public class NMContext:DbContext
    {
        public const string connectionString = "server=localhost;port=3306;user=root;password=Passw0rd;database=network_measurement";
        public NMContext( DbContextOptions<NMContext> option): base(option)
        {
        }
        public NMContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion);
            }
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<MeasurementReport> Measurement_Reports { get; set; }
    }
    public class PanelChangerContextFactory : IDesignTimeDbContextFactory<NMContext>
    {
        public NMContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NMContext>();
            optionsBuilder.UseMySql(NMContext.connectionString, ServerVersion.AutoDetect(NMContext.connectionString));

            return new NMContext(optionsBuilder.Options);
        }
    }
}
