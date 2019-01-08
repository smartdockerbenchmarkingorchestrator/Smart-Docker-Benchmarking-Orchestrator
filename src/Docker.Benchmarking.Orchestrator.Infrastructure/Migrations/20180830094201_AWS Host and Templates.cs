using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class AWSHostandTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWSTemplates",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Template = table.Column<string>(nullable: false),
                    vCPUs = table.Column<double>(nullable: false),
                    InstanceName = table.Column<string>(nullable: false),
                    Memory = table.Column<double>(nullable: false),
                    DiskSize = table.Column<double>(nullable: false),
                    PricePerHour = table.Column<decimal>(nullable: false),
                    DeploymentType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWSTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AWSHosts",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    DockerHostId = table.Column<Guid>(nullable: true),
                    ResourceCreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ResourceDestroyedAt = table.Column<DateTimeOffset>(nullable: false),
                    DestroyResourcesAfterBenchmark = table.Column<bool>(nullable: false),
                    TemplateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWSHosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AWSHosts_DockerHosts_DockerHostId",
                        column: x => x.DockerHostId,
                        principalTable: "DockerHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AWSHosts_AWSTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "AWSTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AWSTemplateParameters",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParameterKey = table.Column<string>(nullable: false),
                    ParameterValue = table.Column<string>(nullable: false),
                    AWSTemplateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWSTemplateParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AWSTemplateParameters_AWSTemplates_AWSTemplateId",
                        column: x => x.AWSTemplateId,
                        principalTable: "AWSTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWSHosts_DockerHostId",
                table: "AWSHosts",
                column: "DockerHostId");

            migrationBuilder.CreateIndex(
                name: "IX_AWSHosts_TemplateId",
                table: "AWSHosts",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AWSTemplateParameters_AWSTemplateId",
                table: "AWSTemplateParameters",
                column: "AWSTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWSHosts");

            migrationBuilder.DropTable(
                name: "AWSTemplateParameters");

            migrationBuilder.DropTable(
                name: "AWSTemplates");
        }
    }
}
