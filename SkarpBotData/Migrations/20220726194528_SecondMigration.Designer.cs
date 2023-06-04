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
    [Migration("20220726194528_SecondMigration")]
    partial class SecondMigration
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

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("GrenadeId");

                    b.HasIndex("UsersId");

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

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("StatusId");

                    b.HasIndex("UsersId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<ulong>("GuildsId")
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("GuildsId1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong>("UserDiscordID")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("GuildsId1");

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

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.Property<string>("WeaponName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("WeaponId");

                    b.HasIndex("UsersId");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Grenades", b =>
                {
                    b.HasOne("SkarpBot.Data.Models.User", "User")
                        .WithMany("Grenades")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Status", b =>
                {
                    b.HasOne("SkarpBot.Data.Models.User", "User")
                        .WithMany("Status")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.User", b =>
                {
                    b.HasOne("SkarpBot.Data.Models.Guilds", "Guilds")
                        .WithMany("User")
                        .HasForeignKey("GuildsId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guilds");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Weapons", b =>
                {
                    b.HasOne("SkarpBot.Data.Models.User", "User")
                        .WithMany("Weapons")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.Guilds", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("SkarpBot.Data.Models.User", b =>
                {
                    b.Navigation("Grenades");

                    b.Navigation("Status");

                    b.Navigation("Weapons");
                });
#pragma warning restore 612, 618
        }
    }
}
