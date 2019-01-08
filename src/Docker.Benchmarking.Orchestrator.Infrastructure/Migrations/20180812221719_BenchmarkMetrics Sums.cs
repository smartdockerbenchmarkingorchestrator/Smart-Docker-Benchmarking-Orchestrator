using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class BenchmarkMetricsSums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BlockInputTotal",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BlockOutputTotal",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUPercentageHighest",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUPercentageLowest",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUPercentageMean",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUPercentageRange",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Memory",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MemoryPercentageHighest",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MemoryPercentageLowest",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MemoryPercentageMean",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MemoryPercentageRange",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetworkInputTotalBytes",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetworkOutputTotalBytes",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "vCPU",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockInputTotal",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "BlockOutputTotal",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "CPUPercentageHighest",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "CPUPercentageLowest",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "CPUPercentageMean",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "CPUPercentageRange",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MemoryPercentageHighest",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MemoryPercentageLowest",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MemoryPercentageMean",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "MemoryPercentageRange",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "NetworkInputTotalBytes",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "NetworkOutputTotalBytes",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "vCPU",
                table: "BenchmarkExperiments");
        }
    }
}
