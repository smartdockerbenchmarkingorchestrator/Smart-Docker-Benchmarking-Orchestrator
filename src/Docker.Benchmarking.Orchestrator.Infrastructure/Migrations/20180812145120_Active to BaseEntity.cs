using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class ActivetoBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DockerImage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ContainerMetrics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "BenchmarkTestItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AzureHosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ApplicationTests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Application",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ApacheJmeterTestFiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "DockerImage");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ContainerMetrics");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "BenchmarkTestItems");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ApplicationTests");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ApacheJmeterTestFiles");
        }
    }
}
