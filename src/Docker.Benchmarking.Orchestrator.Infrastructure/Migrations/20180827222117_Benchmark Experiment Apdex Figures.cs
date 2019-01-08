using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class BenchmarkExperimentApdexFigures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ApdexFustrated",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ApdexSatisfied",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ApdexScore",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ApdexTSeconds",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ApdexTolerating",
                table: "BenchmarkExperiments",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApdexFustrated",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "ApdexSatisfied",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "ApdexScore",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "ApdexTSeconds",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "ApdexTolerating",
                table: "BenchmarkExperiments");
        }
    }
}
