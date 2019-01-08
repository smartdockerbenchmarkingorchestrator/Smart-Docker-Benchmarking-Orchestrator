using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class NewMetricsforBenchmarkExperiment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageLatecy",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageReceivedBytes",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageResponseTime",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageSentBytes",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUStandDeviation",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUStandDeviationSample",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxLatency",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxResponseTime",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MemoryStandDeviation",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MemoryStandDeviationSample",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinLatency",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinResponseTime",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfErrors",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSamples",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "StandardDeviationSameWebServerResponse",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StandardDeviationWebServerResponse",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalReceivedBytes",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalSentBytes",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WebServerEndTime",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WebServerStartTime",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageLatecy",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "AverageReceivedBytes",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "AverageResponseTime",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "AverageSentBytes",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "CPUStandDeviation",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "CPUStandDeviationSample",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MaxLatency",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MaxResponseTime",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MemoryStandDeviation",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MemoryStandDeviationSample",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MinLatency",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MinResponseTime",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "NumberOfErrors",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "NumberOfSamples",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "StandardDeviationSameWebServerResponse",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "StandardDeviationWebServerResponse",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "TotalReceivedBytes",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "TotalSentBytes",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "WebServerEndTime",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "WebServerStartTime",
                table: "BenchmarkExperiments");
        }
    }
}
