using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.DomainEntities.DBEntities;

namespace Store.DomainEntities;

public partial class StoreSystemContext : DbContext
{
    public StoreSystemContext()
    {
    }

    public StoreSystemContext(DbContextOptions<StoreSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acayear> Acayears { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rolemodule> Rolemodules { get; set; }

    public virtual DbSet<Roleuser> Roleusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=store_system;User=root;Password=0000;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acayear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("acayear");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcaYearcol)
                .HasMaxLength(45)
                .HasColumnName("acaYearcol");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId).HasColumnName("Customer Id");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.EMail)
                .HasMaxLength(45)
                .HasColumnName("E_mail");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Telephone).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PRIMARY");

            entity.ToTable("module");

            entity.Property(e => e.ModuleName).HasMaxLength(45);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.CustomerId, "Customer Id_idx");

            entity.HasIndex(e => e.ProductId, "Product Id_idx");

            entity.Property(e => e.OrderId).HasColumnName("Order Id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer Id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("Order_date");
            entity.Property(e => e.ProductId).HasColumnName("Product Id");
            entity.Property(e => e.Status).HasMaxLength(45);
            entity.Property(e => e.TotalAmount).HasColumnName("Total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Customer Id");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product Id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.Id, "fk_foreign_id");

            entity.Property(e => e.ProductId).HasColumnName("Product Id");
            entity.Property(e => e.BriefDescription)
                .HasMaxLength(45)
                .HasColumnName("Brief Description");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Remark).HasMaxLength(255);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("fk_foreign_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleName).HasMaxLength(45);
        });

        modelBuilder.Entity<Rolemodule>(entity =>
        {
            entity.HasKey(e => e.RoleModuleId).HasName("PRIMARY");

            entity.ToTable("rolemodule");

            entity.HasIndex(e => e.ModuleId, "ModulId_idx");

            entity.HasIndex(e => e.RoleId, "RoleId_idx");

            entity.HasOne(d => d.Module).WithMany(p => p.Rolemodules)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Rolemodules)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleId");
        });

        modelBuilder.Entity<Roleuser>(entity =>
        {
            entity.HasKey(e => e.RoleUserId).HasName("PRIMARY");

            entity.ToTable("roleuser");

            entity.HasIndex(e => e.CustomerId, "CustomerId_idx");

            entity.HasIndex(e => e.RoleId, "RoleId_idx");

            entity.HasOne(d => d.Customer).WithMany(p => p.Roleusers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("CustomerId");

            entity.HasOne(d => d.Role).WithMany(p => p.Roleusers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
