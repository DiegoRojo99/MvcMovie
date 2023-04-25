﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

#nullable disable

namespace McvMovie.Migrations
{
    [DbContext(typeof(MvcMovieContext))]
    partial class MvcMovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("MvcMovie.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("MvcMovie.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MvcMovie.Models.Star", b =>
                {
                    b.Property<int>("StarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StarId");

                    b.HasIndex("ActorId");

                    b.HasIndex("MovieId");

                    b.ToTable("Star");
                });

            modelBuilder.Entity("MvcMovie.Models.Star", b =>
                {
                    b.HasOne("MvcMovie.Models.Actor", "Actor")
                        .WithMany("Movies")
                        .HasForeignKey("ActorId");

                    b.HasOne("MvcMovie.Models.Movie", "Movie")
                        .WithMany("Actors")
                        .HasForeignKey("MovieId");

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MvcMovie.Models.Actor", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MvcMovie.Models.Movie", b =>
                {
                    b.Navigation("Actors");
                });
#pragma warning restore 612, 618
        }
    }
}
