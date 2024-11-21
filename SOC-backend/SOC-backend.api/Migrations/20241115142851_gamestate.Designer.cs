﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOC_backend.data;

#nullable disable

namespace SOC_backend.api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241115142851_gamestate")]
    partial class gamestate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SOC_backend.logic.Models.Cards.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<int>("DMG")
                        .HasColumnType("int");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.GameState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<bool>("PlayersTurn")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("GameState");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.Opponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<int>("GameStateId")
                        .HasColumnType("int");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameStateId");

                    b.HasIndex("ShopId");

                    b.ToTable("Opponent");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.OpponentCard", b =>
                {
                    b.Property<int>("OpponentId")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOffence")
                        .HasColumnType("bit");

                    b.HasKey("OpponentId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("OpponentCard");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.ShopCard", b =>
                {
                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.HasKey("ShopId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("ShopCard");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Player.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.Opponent", b =>
                {
                    b.HasOne("SOC_backend.logic.Models.Match.GameState", null)
                        .WithMany("Players")
                        .HasForeignKey("GameStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SOC_backend.logic.Models.Match.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.OpponentCard", b =>
                {
                    b.HasOne("SOC_backend.logic.Models.Cards.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SOC_backend.logic.Models.Match.Opponent", "Opponent")
                        .WithMany("Cards")
                        .HasForeignKey("OpponentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Opponent");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.ShopCard", b =>
                {
                    b.HasOne("SOC_backend.logic.Models.Cards.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SOC_backend.logic.Models.Match.Shop", "Shop")
                        .WithMany("AvailableCards")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.GameState", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.Opponent", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("SOC_backend.logic.Models.Match.Shop", b =>
                {
                    b.Navigation("AvailableCards");
                });
#pragma warning restore 612, 618
        }
    }
}