﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeItUpData.Library.DataAccess;

namespace TimeItUpData.Library.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20211006160004_InitTimeItUpEFDb")]
    partial class InitTimeItUpEFDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeItUpData.Library.Models.Alarm", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimerId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimerUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "TimerId");

                    b.HasIndex("TimerId1", "TimerUserId");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.Split", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimerId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimerUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "TimerId");

                    b.HasIndex("TimerId1", "TimerUserId");

                    b.ToTable("Splits");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.Timer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Timers");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.Alarm", b =>
                {
                    b.HasOne("TimeItUpData.Library.Models.Timer", "Timer")
                        .WithMany("Alarms")
                        .HasForeignKey("TimerId1", "TimerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Timer");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.Split", b =>
                {
                    b.HasOne("TimeItUpData.Library.Models.Timer", "Timer")
                        .WithMany("Splits")
                        .HasForeignKey("TimerId1", "TimerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Timer");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.Timer", b =>
                {
                    b.HasOne("TimeItUpData.Library.Models.User", "User")
                        .WithMany("Timers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.Timer", b =>
                {
                    b.Navigation("Alarms");

                    b.Navigation("Splits");
                });

            modelBuilder.Entity("TimeItUpData.Library.Models.User", b =>
                {
                    b.Navigation("Timers");
                });
#pragma warning restore 612, 618
        }
    }
}
