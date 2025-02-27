﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechChallengeFiap.Infra.Data.Context;

#nullable disable

namespace TechChallengeFiap.Infra.Data.Migrations
{
    [DbContext(typeof(ConsultaAcoesDBContext))]
    partial class ConsultaAcoesDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AcaoUsuario", b =>
                {
                    b.Property<int>("AcoesId")
                        .HasColumnType("int");

                    b.Property<int>("UsuariosId")
                        .HasColumnType("int");

                    b.HasKey("AcoesId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("AcaoUsuario");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.Acao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataValor")
                        .HasColumnType("datetime2");

                    b.Property<string>("Simbolo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorAcao")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Acoes");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.ConsultaAcoes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataConsulta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ConsultaAcoes");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.PedidoAcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Id_Acao")
                        .HasColumnType("int");

                    b.Property<int>("Id_Usuario")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorAcao")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("dtAprovacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtPedido")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("qtPedido")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Acao");

                    b.HasIndex("Id_Usuario");

                    b.ToTable("PedidoAcoes");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("Permissao")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AcaoUsuario", b =>
                {
                    b.HasOne("TechChallengeFiap.Domain.Entities.Acao", null)
                        .WithMany()
                        .HasForeignKey("AcoesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechChallengeFiap.Domain.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.ConsultaAcoes", b =>
                {
                    b.HasOne("TechChallengeFiap.Domain.Entities.Usuario", "Usuario")
                        .WithMany("ConsultasAcoes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.PedidoAcao", b =>
                {
                    b.HasOne("TechChallengeFiap.Domain.Entities.Acao", "Acao")
                        .WithMany("PedidosAcao")
                        .HasForeignKey("Id_Acao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechChallengeFiap.Domain.Entities.Usuario", "usuario")
                        .WithMany("PedidosAcoes")
                        .HasForeignKey("Id_Usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acao");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.Acao", b =>
                {
                    b.Navigation("PedidosAcao");
                });

            modelBuilder.Entity("TechChallengeFiap.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("ConsultasAcoes");

                    b.Navigation("PedidosAcoes");
                });
#pragma warning restore 612, 618
        }
    }
}
