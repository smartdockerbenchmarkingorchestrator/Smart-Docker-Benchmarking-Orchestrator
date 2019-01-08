using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class NewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostType",
                table: "DockerHosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ResourceCreationStarted",
                table: "AzureHosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ResourceCreationStarted",
                table: "AWSHosts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostType",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "ResourceCreationStarted",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "ResourceCreationStarted",
                table: "AWSHosts");
        }
    }
}
