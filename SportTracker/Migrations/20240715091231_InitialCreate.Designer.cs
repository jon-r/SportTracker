﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportTracker.Server.Models;

#nullable disable

namespace SportTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240715091231_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("SportTracker.Shared.Models.SportEvent", b =>
                {
                    b.Property<int>("SportEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Laps")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UploadTimestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("SportEventId");

                    b.ToTable("SportEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
