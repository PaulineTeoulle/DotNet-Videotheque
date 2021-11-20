﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

namespace MvcMovie.Migrations
{
    [DbContext(typeof(MvcMovieContext))]
    [Migration("20211103130432_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MvcMovie.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdresseClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrenomClient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("MvcMovie.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategorieFilm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateSortieFilm")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DisponibiliteFilm")
                        .HasColumnType("bit");

                    b.Property<int>("NbLocationsFilm")
                        .HasColumnType("int");

                    b.Property<string>("NomFilm")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RealisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RealisateurId");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("MvcMovie.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRetourLocation")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<bool>("RenduFilm")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("FilmId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("MvcMovie.Models.Realisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomRealisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrenomRealisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Realisateur");
                });

            modelBuilder.Entity("MvcMovie.Models.Film", b =>
                {
                    b.HasOne("MvcMovie.Models.Realisateur", "FilmRealisateurId")
                        .WithMany()
                        .HasForeignKey("RealisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FilmRealisateurId");
                });

            modelBuilder.Entity("MvcMovie.Models.Location", b =>
                {
                    b.HasOne("MvcMovie.Models.Client", "LocationClientId")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MvcMovie.Models.Film", "LocationFilmId")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationClientId");

                    b.Navigation("LocationFilmId");
                });
#pragma warning restore 612, 618
        }
    }
}
