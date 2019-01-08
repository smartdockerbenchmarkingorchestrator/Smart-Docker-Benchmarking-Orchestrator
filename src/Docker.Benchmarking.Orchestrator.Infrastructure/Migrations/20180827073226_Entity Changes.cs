using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class EntityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResourceName",
                table: "AzureHosts",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContainerMetrics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BenchmarkTestItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContainerMetrics");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BenchmarkTestItems");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AzureHosts",
                newName: "ResourceName");
        }
    }
}
