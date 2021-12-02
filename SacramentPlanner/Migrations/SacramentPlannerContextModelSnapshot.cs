﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SacramentPlanner.Data;

namespace SacramentPlanner.Migrations
{
    [DbContext(typeof(SacramentPlannerContext))]
    partial class SacramentPlannerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SacramentPlanner.Models.Hymn", b =>
                {
                    b.Property<int>("HymnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HymnId");

                    b.ToTable("Hymn");
                });

            modelBuilder.Entity("SacramentPlanner.Models.SacramentPlan", b =>
                {
                    b.Property<int>("SacramentPlannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClosingHymnHymnId")
                        .HasColumnType("int");

                    b.Property<string>("ClosingPrayer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conducting")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IntermediateHymnHymnId")
                        .HasColumnType("int");

                    b.Property<int?>("OpeningHymnHymnId")
                        .HasColumnType("int");

                    b.Property<string>("OpeningPrayer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Presiding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SacramentHymnHymnId")
                        .HasColumnType("int");

                    b.HasKey("SacramentPlannerId");

                    b.HasIndex("ClosingHymnHymnId");

                    b.HasIndex("IntermediateHymnHymnId");

                    b.HasIndex("OpeningHymnHymnId");

                    b.HasIndex("SacramentHymnHymnId");

                    b.ToTable("SacramentPlanner");
                });

            modelBuilder.Entity("SacramentPlanner.Models.SacramentPlan", b =>
                {
                    b.HasOne("SacramentPlanner.Models.Hymn", "ClosingHymn")
                        .WithMany()
                        .HasForeignKey("ClosingHymnHymnId");

                    b.HasOne("SacramentPlanner.Models.Hymn", "IntermediateHymn")
                        .WithMany()
                        .HasForeignKey("IntermediateHymnHymnId");

                    b.HasOne("SacramentPlanner.Models.Hymn", "OpeningHymn")
                        .WithMany()
                        .HasForeignKey("OpeningHymnHymnId");

                    b.HasOne("SacramentPlanner.Models.Hymn", "SacramentHymn")
                        .WithMany()
                        .HasForeignKey("SacramentHymnHymnId");

                    b.Navigation("ClosingHymn");

                    b.Navigation("IntermediateHymn");

                    b.Navigation("OpeningHymn");

                    b.Navigation("SacramentHymn");
                });
#pragma warning restore 612, 618
        }
    }
}
