﻿// <auto-generated />
using dashboard.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Dashboard.Migrations
{
    [DbContext(typeof(DashboardDbContext))]
    [Migration("20171006174214_AddTeamEnvironmentClientGroupRelation")]
    partial class AddTeamEnvironmentClientGroupRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("dashboard.Core.Models.ClientGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ClientGroups");
                });

            modelBuilder.Entity("dashboard.Core.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("dashboard.Core.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("GoalEnd");

                    b.Property<DateTime>("GoalStart");

                    b.Property<string>("SprintCode");

                    b.Property<int?>("TeamMemberId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("dashboard.Core.Models.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("dashboard.Core.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MakeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("dashboard.Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("dashboard.Core.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("dashboard.Core.Models.TeamEnvironment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientGroupId");

                    b.Property<string>("Description");

                    b.Property<string>("ExtraDetails");

                    b.Property<string>("Name");

                    b.Property<int?>("TeamId");

                    b.Property<int?>("TeamMemberId");

                    b.HasKey("Id");

                    b.HasIndex("ClientGroupId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("TeamEnvironments");
                });

            modelBuilder.Entity("dashboard.Core.Models.TeamMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MiddleNames")
                        .HasMaxLength(50);

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("dashboard.Core.Models.UpcomingEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EventDate");

                    b.Property<int?>("TeamId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("UpcomingEvents");
                });

            modelBuilder.Entity("dashboard.Core.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<bool>("IsRegistered");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("ModelId");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("dashboard.Core.Models.VehicleFeature", b =>
                {
                    b.Property<int>("VehicleId");

                    b.Property<int>("FeatureId");

                    b.HasKey("VehicleId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("VehicleFeatures");
                });

            modelBuilder.Entity("dashboard.Core.Models.Goal", b =>
                {
                    b.HasOne("dashboard.Core.Models.TeamMember")
                        .WithMany("Goals")
                        .HasForeignKey("TeamMemberId");
                });

            modelBuilder.Entity("dashboard.Core.Models.Model", b =>
                {
                    b.HasOne("dashboard.Core.Models.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dashboard.Core.Models.Photo", b =>
                {
                    b.HasOne("dashboard.Core.Models.Vehicle")
                        .WithMany("Photos")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dashboard.Core.Models.TeamEnvironment", b =>
                {
                    b.HasOne("dashboard.Core.Models.ClientGroup", "ClientGroup")
                        .WithMany()
                        .HasForeignKey("ClientGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dashboard.Core.Models.Team")
                        .WithMany("TeamEnvironments")
                        .HasForeignKey("TeamId");

                    b.HasOne("dashboard.Core.Models.TeamMember")
                        .WithMany("TeamEnvironments")
                        .HasForeignKey("TeamMemberId");
                });

            modelBuilder.Entity("dashboard.Core.Models.TeamMember", b =>
                {
                    b.HasOne("dashboard.Core.Models.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dashboard.Core.Models.UpcomingEvent", b =>
                {
                    b.HasOne("dashboard.Core.Models.Team")
                        .WithMany("UpcomingEvents")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("dashboard.Core.Models.Vehicle", b =>
                {
                    b.HasOne("dashboard.Core.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dashboard.Core.Models.VehicleFeature", b =>
                {
                    b.HasOne("dashboard.Core.Models.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dashboard.Core.Models.Vehicle", "Vehicle")
                        .WithMany("Features")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
