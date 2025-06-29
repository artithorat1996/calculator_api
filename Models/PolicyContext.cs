using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class PolicyContext : DbContext
{
    public PolicyContext()
    {
    }

    public PolicyContext(DbContextOptions<PolicyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InsuredDetail> InsuredDetails { get; set; }

    public virtual DbSet<PolicyDetail> PolicyDetails { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LT0166801\\SQLEXPRESS01; Database=POLICY; User Id=shantanu; Password=shantanu@123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InsuredDetail>(entity =>
        {
            entity.ToTable("INSURED_DETAILS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AgeInYears).HasColumnName("AGE_IN_YEARS");
            entity.Property(e => e.Dob)
                .HasMaxLength(10)
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Mobile)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MOBILE");
        });

        modelBuilder.Entity<PolicyDetail>(entity =>
        {
            entity.ToTable("POLICY_DETAILS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("END_DATE");
            entity.Property(e => e.InsuredId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("INSURED_ID");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(50)
                .HasColumnName("POLICY_NUMBER");
            entity.Property(e => e.Scheme)
                .HasMaxLength(50)
                .HasColumnName("SCHEME");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Tenure).HasColumnName("TENURE");
            entity.Property(e => e.YearlyPremium)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("YEARLY_PREMIUM");

            entity.HasOne(d => d.Insured).WithMany(p => p.PolicyDetails)
                .HasForeignKey(d => d.InsuredId)
                .HasConstraintName("FK_POLICY_DETAILS_INSURED_DETAILS");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("USER_DETAILS");

            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserId).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
