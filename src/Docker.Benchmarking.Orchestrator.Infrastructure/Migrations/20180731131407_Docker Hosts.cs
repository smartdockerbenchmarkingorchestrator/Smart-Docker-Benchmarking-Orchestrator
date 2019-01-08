using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class DockerHosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CloudProvider",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DockerHosts",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HostName = table.Column<string>(nullable: false),
                    PortName = table.Column<string>(nullable: false),
                    UseHttpAuthentication = table.Column<bool>(nullable: false),
                    UseTlsAuthentication = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CloudProviderId = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerHosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DockerHosts_CloudProvider_CloudProviderId",
                        column: x => x.CloudProviderId,
                        principalTable: "CloudProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DockerHosts_CloudProviderId",
                table: "DockerHosts",
                column: "CloudProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DockerHosts");

            migrationBuilder.DropTable(
                name: "CloudProvider");
        }
    }
}
