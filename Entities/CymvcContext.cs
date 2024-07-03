using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CyMvc.Entities;

public partial class CymvcContext : DbContext
{
    public CymvcContext()
    {
    }

    public CymvcContext(DbContextOptions<CymvcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientInfo> ClientInfos { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Cymvc;TrustServerCertificate=true;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientInfo>(entity =>
        {
            entity.ToTable("ClientInfo");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CivilStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameOfFather)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameOfMother)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Occupation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.ToTable("Loan");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Collectable).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Collected).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DateCreated).HasColumnType("date");
            entity.Property(e => e.Deduct).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Interest).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.InterestAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Recievable).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalPayable).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.Collectable).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Schedule).HasColumnType("date");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.PaymentId).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
