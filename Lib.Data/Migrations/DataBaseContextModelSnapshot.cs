﻿// <auto-generated />
using System;
using Lib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lib.Data.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Lib.Data.CargoDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("Lib.Data.ChamadoContaDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Funcao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdContaAtribuido")
                        .HasColumnType("int");

                    b.Property<int>("IdContaAtribuinte")
                        .HasColumnType("int");

                    b.Property<int>("IdProjeto")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ChamadoConta");
                });

            modelBuilder.Entity("Lib.Data.ChamadoDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Arquivado")
                        .HasColumnType("bit");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdProjeto")
                        .HasColumnType("int");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoChamado")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProjeto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Chamados");
                });

            modelBuilder.Entity("Lib.Data.ContaCargoDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("IdCargo")
                        .HasColumnType("int");

                    b.Property<int>("IdConta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ContaCargo");
                });

            modelBuilder.Entity("Lib.Data.ContaDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Conta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataDeCriacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("Lib.Data.ContaProjetoDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Funcao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GerenteDeProjeto")
                        .HasColumnType("bit");

                    b.Property<int>("IdConta")
                        .HasColumnType("int");

                    b.Property<int>("IdProjeto")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ContaProjeto");
                });

            modelBuilder.Entity("Lib.Data.EconomiasDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEconomiaMeta")
                        .HasColumnType("int");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<string>("NomeEconomiaMeta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Economias");
                });

            modelBuilder.Entity("Lib.Data.EconomiasMetaDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("EconomiasMetas");
                });

            modelBuilder.Entity("Lib.Data.ProjetoDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCriador")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("Lib.Data.TarefaDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Dia")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Feita")
                        .HasColumnType("bit");

                    b.Property<DateTime>("HoraDeConclusao")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdConta")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
