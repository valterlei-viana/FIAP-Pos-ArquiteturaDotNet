﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechChallengeFIAP.Infrastructure.Data;

#nullable disable

namespace TechChallengeFIAP.API.Migrations
{
    [DbContext(typeof(FiapDbContext))]
    [Migration("20240915194630_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TechChallengeFIAP.Core.Entities.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TelefoneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TelefoneId");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("TechChallengeFIAP.Core.Entities.Telefone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DDD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UF")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("TechChallengeFIAP.Core.Entities.Contato", b =>
                {
                    b.HasOne("TechChallengeFIAP.Core.Entities.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("TelefoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Telefone");
                });
#pragma warning restore 612, 618
        }
    }
}
