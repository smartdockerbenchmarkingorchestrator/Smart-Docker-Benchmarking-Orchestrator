using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class PortNametoPortNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortName",
                table: "DockerHosts");

            migrationBuilder.AddColumn<int>(
                name: "PortNumber",
                table: "DockerHosts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortNumber",
                table: "DockerHosts");

            migrationBuilder.AddColumn<string>(
                name: "PortName",
                table: "DockerHosts",
                nullable: false,
                defaultValue: "");
        }
    }
}
