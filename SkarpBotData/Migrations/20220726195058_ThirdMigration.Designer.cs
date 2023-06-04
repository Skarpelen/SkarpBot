﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkarpBot.Data.Context;

#nullable disable

namespace SkarpBot.Data.Migrations
{
    [DbContext(typeof(SkarpBotDbContext))]
    [Migration("20220726195058_ThirdMigration")]
    partial class ThirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SkarpBot.Data.Models.Grenades", b =>
                {
                    b.Property<int>("GrenadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("GrenadeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GrenadeId");

                    b.ToTable("Grenades");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Guilds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong>("ServerId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Armour")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Body")
                        .HasColumnType("int");

                    b.Property<int>("Head")
                        .HasColumnType("int");

                    b.Property<int>("LFoot")
                        .HasColumnType("int");

                    b.Property<int>("LHand")
                        .HasColumnType("int");

                    b.Property<int>("RFoot")
                        .HasColumnType("int");

                    b.Property<int>("RHand")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("GuildsId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong>("UserDiscordID")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Weapons", b =>
                {
                    b.Property<int>("WeaponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CurrentAmmo")
                        .HasColumnType("int");

                    b.Property<string>("InventoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WeaponName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("WeaponId");

                    b.ToTable("Weapons");
                });
#pragma warning restore 612, 618
        }
    }
}
