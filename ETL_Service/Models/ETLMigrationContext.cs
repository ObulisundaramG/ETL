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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=VisualStudio1;Database=ETLMigration;User Id=sa;Password=sa@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfaConnector>(entity =>
            {
                entity.HasKey(e => e.ConnectorId)
                    .HasName("PK__INFA_CON__51035FB73C238C3B");

                entity.ToTable("INFA_CONNECTOR");

                entity.Property(e => e.ConnectorId).HasColumnName("CONNECTOR_ID");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FldrId).HasColumnName("FLDR_ID");

                entity.Property(e => e.MappingId).HasColumnName("MAPPING_ID");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("MODIFIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RepId).HasColumnName("REP_ID");

                entity.Property(e => e.SessionId).HasColumnName("SESSION_ID");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.WkfId).HasColumnName("WKF_ID");

                entity.HasOne(d => d.Fldr)
                    .WithMany(p => p.InfaConnector)
                    .HasForeignKey(d => d.FldrId)
                    .HasConstraintName("FK__INFA_CONN__FLDR___3D5E1FD2");

                entity.HasOne(d => d.Mapping)
                    .WithMany(p => p.InfaConnector)
                    .HasForeignKey(d => d.MappingId)
                    .HasConstraintName("FK__INFA_CONN__MAPPI__3F466844");

                entity.HasOne(d => d.Rep)
                    .WithMany(p => p.InfaConnector)
                    .HasForeignKey(d => d.RepId)
                    .HasConstraintName("FK__INFA_CONN__MODIF__3B75D760");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.InfaConnector)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__INFA_CONN__SESSI__3C69FB99");

                entity.HasOne(d => d.Wkf)
                    .WithMany(p => p.InfaConnector)
                    .HasForeignKey(d => d.WkfId)
                    .HasConstraintName("FK__INFA_CONN__WKF_I__3E52440B");
            });

            modelBuilder.Entity<InfaFolder>(entity =>
            {
                entity.HasKey(e => e.FldrId)
                    .HasName("PK__INFA_FOL__F674F195EA17A464");

                entity.ToTable("INFA_FOLDER");

                entity.Property(e => e.FldrId).HasColumnName("FLDR_ID");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FldrDesc)
                    .HasColumnName("FLDR_DESC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FldrName)
                    .HasColumnName("FLDR_NAME")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("MODIFIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RepId).HasColumnName("REP_ID");

                entity.Property(e => e.Shared)
                    .HasColumnName("SHARED")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Rep)
                    .WithMany(p => p.InfaFolder)
                    .HasForeignKey(d => d.RepId)
                    .HasConstraintName("FK__INFA_FOLD__MODIF__2B3F6F97");
            });

            modelBuilder.Entity<InfaMapping>(entity =>
            {
                entity.HasKey(e => e.MappingId)
                    .HasName("PK__INFA_MAP__9476A052BB868078");

                entity.ToTable("INFA_MAPPING");

                entity.Property(e => e.MappingId).HasColumnName("MAPPING_ID");

                entity.Property(e => e.AssociatedSourceInstance)
                    .HasColumnName("ASSOCIATED_SOURCE_INSTANCE")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Datatype)
                    .HasColumnName("DATATYPE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Expression)
                    .HasColumnName("EXPRESSION")
                    .HasMaxLength(999)
                    .IsUnicode(false);

                entity.Property(e => e.ExpressionType)
                    .HasColumnName("EXPRESSION_TYPE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FieldDesc)
                    .HasColumnName("FIELD_DESC")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasColumnName("FIELD_NAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Flag)
                    .HasColumnName("FLAG")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FldrId).HasColumnName("FLDR_ID");

                entity.Property(e => e.InstanceName)
                    .HasColumnName("INSTANCE_NAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.InstanceType)
                    .HasColumnName("INSTANCE_TYPE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LoadTimestamp)
                    .HasColumnName("LOAD_TIMESTAMP")
                    .HasColumnType("date");

                entity.Property(e => e.MappingDesc)
                    .HasColumnName("MAPPING_DESC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MappingIsvalid)
                    .HasColumnName("MAPPING_ISVALID")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MappingName)
                    .HasColumnName("MAPPING_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("MODIFIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Porttype)
                    .HasColumnName("PORTTYPE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Precision).HasColumnName("PRECISION");

                entity.Property(e => e.Scale).HasColumnName("SCALE");

                entity.Property(e => e.SessionId).HasColumnName("SESSION_ID");

                entity.Property(e => e.SortDirection)
                    .HasColumnName("SORT_DIRECTION")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.SortKey)
                    .HasColumnName("SORT_KEY")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TableattributeName)
                    .HasColumnName("TABLEATTRIBUTE_NAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TableattributeValue)
                    .HasColumnName("TABLEATTRIBUTE_VALUE")
                    .HasMaxLength(999)
                    .IsUnicode(false);

                entity.Property(e => e.TargetInstance)
                    .HasColumnName("TARGET_INSTANCE")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TargetLoadorder)
                    .HasColumnName("TARGET_LOADORDER")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TransName)
                    .HasColumnName("TRANS_NAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TransReusable)
                    .HasColumnName("TRANS_REUSABLE")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TransType)
                    .HasColumnName("TRANS_TYPE")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedTimestamp)
                    .HasColumnName("UPDATED_TIMESTAMP")
                    .HasColumnType("date");

                entity.HasOne(d => d.Fldr)
                    .WithMany(p => p.InfaMapping)
                    .HasForeignKey(d => d.FldrId)
                    .HasConstraintName("FK__INFA_MAPP__FLDR___37A5467C");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.InfaMapping)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__INFA_MAPP__MODIF__36B12243");
            });

            modelBuilder.Entity<InfaRepository>(entity =>
            {
                entity.HasKey(e => e.RepId)
                    .HasName("PK__INFA_REP__D5D970281B758467");

                entity.ToTable("INFA_REPOSITORY");

                entity.Property(e => e.RepId).HasColumnName("REP_ID");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DatabaseType)
                    .HasColumnName("DATABASE_TYPE")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("MODIFIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RepDesc)
                    .HasColumnName("REP_DESC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RepName)
                    .HasColumnName("REP_NAME")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<InfaSession>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK__INFA_SES__F017487457974C71");

                entity.ToTable("INFA_SESSION");

                entity.Property(e => e.SessionId).HasColumnName("SESSION_ID");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("MODIFIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionDesc)
                    .HasColumnName("SESSION_DESC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SessionName)
                    .HasColumnName("SESSION_NAME")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.WkfId).HasColumnName("WKF_ID");

                entity.HasOne(d => d.Wkf)
                    .WithMany(p => p.InfaSession)
                    .HasForeignKey(d => d.WkfId)
                    .HasConstraintName("FK__INFA_SESS__MODIF__32E0915F");
            });

            modelBuilder.Entity<InfaWorkflow>(entity =>
            {
                entity.HasKey(e => e.WkfId)
                    .HasName("PK__INFA_WOR__99B413C27649645D");

                entity.ToTable("INFA_WORKFLOW");

                entity.Property(e => e.WkfId).HasColumnName("WKF_ID");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("CREATED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FldrId).HasColumnName("FLDR_ID");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("MODIFIED_BY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("MODIFIED_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.WkfCol)
                    .HasColumnName("WKF_COL")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.WkfDesc)
                    .HasColumnName("WKF_DESC")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WkfName)
                    .HasColumnName("WKF_NAME")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fldr)
                    .WithMany(p => p.InfaWorkflow)
                    .HasForeignKey(d => d.FldrId)
                    .HasConstraintName("FK__INFA_WORK__MODIF__2F10007B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
