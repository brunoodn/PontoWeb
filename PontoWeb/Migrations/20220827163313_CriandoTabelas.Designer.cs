﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PontoWeb.Data;

#nullable disable

namespace PontoWeb.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20220827163313_CriandoTabelas")]
    partial class CriandoTabelas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PontoWeb.Models.BatidaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("FuncionarioMatricula")
                        .HasColumnType("int");

                    b.Property<int?>("MatriculaSupervisorAjuste")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("Registro")
                        .HasColumnType("datetime2");

                    b.Property<int>("TipoBatida")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioMatricula");

                    b.ToTable("Batidas");
                });

            modelBuilder.Entity("PontoWeb.Models.FuncionarioModel", b =>
                {
                    b.Property<int>("Matricula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Matricula"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NIS")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Matricula");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("PontoWeb.Models.BatidaModel", b =>
                {
                    b.HasOne("PontoWeb.Models.FuncionarioModel", "Funcionario")
                        .WithMany("Batidas")
                        .HasForeignKey("FuncionarioMatricula")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("PontoWeb.Models.FuncionarioModel", b =>
                {
                    b.Navigation("Batidas");
                });
#pragma warning restore 612, 618
        }
    }
}
