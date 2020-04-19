using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ETL_Service.Models
{
    public partial class ETLMigrationContext : DbContext
    {
        public ETLMigrationContext()
        {
        }

        public ETLMigrationContext(DbContextOptions<ETLMigrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InfaConnector> InfaConnector { get; set; }
        public virtual DbSet<InfaFolder> InfaFolder { get; set; }
        public virtual DbSet<InfaMapping> InfaMapping { get; set; }
        public virtual DbSet<InfaRepository> InfaRepository { get; set; }
        public virtual DbSet<InfaSession> InfaSession { get; set; }
        public virtual DbSet<InfaWorkflow> InfaWorkflow { get; set; }
        public virtual DbSet<InfaSessionConfig> InfaSessionConfig { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=VisualStudio1;Database=ETLMigration;User Id=sa;Password=sa@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        
    }
}
