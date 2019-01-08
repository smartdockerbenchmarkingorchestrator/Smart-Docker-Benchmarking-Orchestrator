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
    [Migration("20180808133605_RemoveJsonResponse")]
    partial class RemoveJsonResponse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ApacheJmeterTestFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationId");

                    b.Property<Guid?>("ApplicationTestId");

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId")
                        .IsUnique();

                    b.HasIndex("ApplicationTestId")
                        .IsUnique();

                    b.ToTable("ApacheJmeterTestFiles");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApacheJmeterTestId");

                    b.Property<Guid?>("ApplicationImageId");

                    b.Property<int>("ApplicationType");

                    b.Property<Guid?>("BenchmarkingImageId");

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<int>("DelayToStartApplication");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationImageId");

                    b.HasIndex("BenchmarkingImageId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApacheJmeterTestId");

                    b.Property<string>("AppContainerId");

                    b.Property<Guid>("ApplicationHostId");

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("BenchmarkContainerId");

                    b.Property<Guid>("BenchmarkHostId");

                    b.Property<int>("BenchmarkTimeLength");

                    b.Property<bool>("CaptureContainerMetrics");

                    b.Property<string>("Comments");

                    b.Property<bool>("Completed");

                    b.Property<DateTimeOffset>("CompletedAt");

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Started");

                    b.Property<DateTimeOffset>("StartedAt");

                    b.Property<bool>("StopAppContainerAfterExperiment");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationHostId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("BenchmarkHostId");

                    b.ToTable("ApplicationTests");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTestEnvironmentVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationTestId");

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationTestId");

                    b.ToTable("ApplicationTestEnvironmentVariable");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.BenchmarkTestItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AllThreads");

                    b.Property<Guid?>("ApplicationTestId");

                    b.Property<double>("Bytes");

                    b.Property<double>("Connect");

                    b.Property<string>("DataType");

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<double>("Elapsed");

                    b.Property<string>("FailureMessage");

                    b.Property<double>("GroupThreads");

                    b.Property<double>("IdleTime");

                    b.Property<string>("Label")
                        .IsRequired();

                    b.Property<double>("Latency");

                    b.Property<int?>("ResponseCode");

                    b.Property<string>("ResponseMessage")
                        .IsRequired();

                    b.Property<double>("SentBytes");

                    b.Property<bool>("Success");

                    b.Property<string>("ThreadName")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationTestId");

                    b.ToTable("BenchmarkTestItems");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.CloudProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CloudProviders");

                    b.HasData(
                        new { Id = new Guid("e966a5f8-66dc-4434-9942-07fd884bfe68"), DateTimeCreated = new DateTimeOffset(new DateTime(2018, 8, 8, 13, 36, 5, 10, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "AWS" },
                        new { Id = new Guid("0a07af89-3067-44ea-97f3-239ac831ac5c"), DateTimeCreated = new DateTimeOffset(new DateTime(2018, 8, 8, 13, 36, 5, 10, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Azure" },
                        new { Id = new Guid("4dd52b8a-17b7-466d-88ff-2bd77c47afc2"), DateTimeCreated = new DateTimeOffset(new DateTime(2018, 8, 8, 13, 36, 5, 10, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Google Cloud Platform" },
                        new { Id = new Guid("2fc3b71f-4188-4620-86f0-b7f445e89f88"), DateTimeCreated = new DateTimeOffset(new DateTime(2018, 8, 8, 13, 36, 5, 10, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "IBM" },
                        new { Id = new Guid("26352b58-0043-4b0f-9183-0bbe965db8a2"), DateTimeCreated = new DateTimeOffset(new DateTime(2018, 8, 8, 13, 36, 5, 10, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), Name = "Private Cloud" }
                    );
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ContainerMetric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationTestId");

                    b.Property<float>("BlockInput");

                    b.Property<float>("BlockOutput");

                    b.Property<float>("CPUPercentage");

                    b.Property<string>("ContainerId")
                        .IsRequired();

                    b.Property<string>("ContainerName")
                        .IsRequired();

                    b.Property<DateTimeOffset>("DateTimeCreated");

                    b.Property<DateTime>("DockerDateTimestamp");

                    b.Property<float>("MemoryLimit");

                    b.Property<float>("MemoryUsage");

                    b.Property<decimal>("NetworkInput")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<decimal>("NetworkOutput")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.HasKey("Id");

                    b.HasIndex("ApplicationTestId");

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

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ApacheJmeterTestFile", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.Application", "Application")
                        .WithOne("TestFile")
                        .HasForeignKey("Docker.Benchmarking.Orchestrator.Core.Entities.ApacheJmeterTestFile", "ApplicationId");

                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTest", "ApplicationTest")
                        .WithOne("TestFile")
                        .HasForeignKey("Docker.Benchmarking.Orchestrator.Core.Entities.ApacheJmeterTestFile", "ApplicationTestId");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.Application", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage", "ApplicationImage")
                        .WithMany()
                        .HasForeignKey("ApplicationImageId");

                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.DockerImage", "BenchmarkingImage")
                        .WithMany()
                        .HasForeignKey("BenchmarkingImageId");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTest", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.DockerHost", "Host")
                        .WithMany("ApplicationTests")
                        .HasForeignKey("ApplicationHostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.Application", "Application")
                        .WithMany("ApplicationTests")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.DockerHost", "BenchmarkHost")
                        .WithMany("BenchmarkTests")
                        .HasForeignKey("BenchmarkHostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTestEnvironmentVariable", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTest", "ApplicationTest")
                        .WithMany("EnvironmentVariables")
                        .HasForeignKey("ApplicationTestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.BenchmarkTestItem", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTest", "ApplicationTest")
                        .WithMany("TestResults")
                        .HasForeignKey("ApplicationTestId");
                });

            modelBuilder.Entity("Docker.Benchmarking.Orchestrator.Core.Entities.ContainerMetric", b =>
                {
                    b.HasOne("Docker.Benchmarking.Orchestrator.Core.Entities.ApplicationTest", "ApplicationTest")
                        .WithMany("ContainerMetrics")
                        .HasForeignKey("ApplicationTestId")
                        .OnDelete(DeleteBehavior.Cascade);
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
