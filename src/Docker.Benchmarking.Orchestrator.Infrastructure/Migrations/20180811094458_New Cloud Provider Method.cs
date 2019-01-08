using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class NewCloudProviderMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CloudProvider",
                table: "DockerHosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DestroyResourcesAfterBenchmark",
                table: "AzureHosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudProvider",
                table: "DockerHosts");

            migrationBuilder.DropColumn(
                name: "DestroyResourcesAfterBenchmark",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AzureHosts");
        }
    }
}
