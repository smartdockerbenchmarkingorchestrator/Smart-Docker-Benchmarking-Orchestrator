using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class FlagsforAWSAzureResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ResourceDestroyed",
                table: "AzureHosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResourcedCreated",
                table: "AzureHosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResourceDestroyed",
                table: "AWSHosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResourcedCreated",
                table: "AWSHosts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceDestroyed",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "ResourcedCreated",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "ResourceDestroyed",
                table: "AWSHosts");

            migrationBuilder.DropColumn(
                name: "ResourcedCreated",
                table: "AWSHosts");
        }
    }
}
