using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class FutherChangestoAzurHost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "Subscription",
                table: "AzureHosts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AzureHosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subscription",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AzureHosts",
                nullable: false,
                defaultValue: "");
        }
    }
}
