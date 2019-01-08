using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class BenchmarkReferenceCommands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BenchmarkingName",
                table: "BenchmarkExperiments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExperimentReference",
                table: "BenchmarkExperiments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BenchmarkingName",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "ExperimentReference",
                table: "BenchmarkExperiments");
        }
    }
}
