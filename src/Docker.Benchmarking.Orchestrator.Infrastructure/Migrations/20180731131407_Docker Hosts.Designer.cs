﻿// <auto-generated />
using System;
using Docker.Benchmarking.Orchestrator.Infrastrcture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180731131407_Docker Hosts")]
    partial class DockerHosts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.CloudProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CloudProvider");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ContainerMetric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContainerId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<DateTime>("DockerDateTimestamp");

                    b.Property<string>("Hostname")
                        .IsRequired();

                    b.Property<decimal>("MemroryUsage")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.HasKey("Id");

                    b.ToTable("ContainerMetrics");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.DockerHost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("CloudProviderId");

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<string>("HostName")
                        .IsRequired();

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<string>("PortName")
                        .IsRequired();

                    b.Property<bool>("UseHttpAuthentication");

                    b.Property<bool>("UseTlsAuthentication");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("CloudProviderId");

                    b.ToTable("DockerHosts");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<int?>("ExternalPort");

                    b.Property<string>("ImageName")
                        .IsRequired();

                    b.Property<string>("ImageTag")
                        .IsRequired();

                    b.Property<int>("ImageType");

                    b.Property<int?>("InternalPort");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("PrivateRepository");

                    b.Property<string>("PrivateRepositoryHost");

                    b.Property<string>("PrivateRepositoryPassword");

                    b.Property<string>("PrivateRepositoryUsername");

                    b.HasKey("Id");

                    b.ToTable("DockerImage");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.DockerHost", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.CloudProvider", "CloudProvider")
                        .WithMany()
                        .HasForeignKey("CloudProviderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
