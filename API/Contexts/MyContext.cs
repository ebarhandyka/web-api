﻿using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public partial class MyContext : DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<AccountRole> AccountRoles { get; set; }

    public virtual DbSet<Profiling> Profilings { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.EmployeeNik);

            entity.ToTable("TB_M_Account");

            entity.Property(e => e.EmployeeNik)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("employee_nik");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.EmployeeNikNavigation).WithOne(p => p.TbMAccount)
                .HasForeignKey<Account>(d => d.EmployeeNik)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable("TB_M_Educations");

            entity.HasIndex(e => e.UniversityId, "IX_TB_M_Educations_university_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Degree)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("degree");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("gpa");
            entity.Property(e => e.Major)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");

            entity.HasOne(d => d.University).WithMany(p => p.TbMEducations)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Nik);

            entity.ToTable("TB_M_Employees");

            entity.HasIndex(e => e.Email, "IX_TB_M_Employees_email").IsUnique();

            entity.Property(e => e.Nik)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nik");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HiringDate)
                .HasColumnType("datetime")
                .HasColumnName("hiring_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("TB_M_Roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.ToTable("TB_M_Universities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.ToTable("TB_TR_Account_Roles");

            entity.HasIndex(e => e.AccountNik, "IX_TB_TR_Account_Roles_account_nik");

            entity.HasIndex(e => e.RoleId, "IX_TB_TR_Account_Roles_role_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNik)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("account_nik");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.AccountNikNavigation).WithMany(p => p.TbTrAccountRoles)
                .HasForeignKey(d => d.AccountNik)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Role).WithMany(p => p.TbTrAccountRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Profiling>(entity =>
        {
            entity.HasKey(e => e.EmployeeNik);

            entity.ToTable("TB_TR_Profilings");

            entity.HasIndex(e => e.EducationId, "IX_TB_TR_Profilings_education_id").IsUnique();

            entity.Property(e => e.EmployeeNik)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("employee_nik");
            entity.Property(e => e.EducationId).HasColumnName("education_id");

            entity.HasOne(d => d.Education).WithOne(p => p.TbTrProfiling)
                .HasForeignKey<Profiling>(d => d.EducationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.EmployeeNikNavigation).WithOne(p => p.TbTrProfiling)
                .HasForeignKey<Profiling>(d => d.EmployeeNik)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
