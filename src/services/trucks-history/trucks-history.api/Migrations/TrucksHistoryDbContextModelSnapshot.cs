﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TrucksHistory.Infrastructure.Postgres;

#nullable disable

namespace trucks_history.api.Migrations
{
    [DbContext(typeof(TrucksHistoryDbContext))]
    partial class TrucksHistoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TrucksHistory.Infrastructure.Postgres.DbModels.TruckHistoryDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StatusCode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TrucksHistory", (string)null);
                });

            modelBuilder.Entity("TrucksHistory.Infrastructure.Postgres.DbModels.TruckHistoryEntryDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StatusCode")
                        .HasColumnType("integer");

                    b.Property<Guid>("TruckId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TruckId");

                    b.ToTable("TruckHistoryEntries");
                });

            modelBuilder.Entity("TrucksHistory.Infrastructure.Postgres.DbModels.TruckHistoryEntryDbModel", b =>
                {
                    b.HasOne("TrucksHistory.Infrastructure.Postgres.DbModels.TruckHistoryDbModel", "TruckHistory")
                        .WithMany("Entries")
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TruckHistory");
                });

            modelBuilder.Entity("TrucksHistory.Infrastructure.Postgres.DbModels.TruckHistoryDbModel", b =>
                {
                    b.Navigation("Entries");
                });
#pragma warning restore 612, 618
        }
    }
}
