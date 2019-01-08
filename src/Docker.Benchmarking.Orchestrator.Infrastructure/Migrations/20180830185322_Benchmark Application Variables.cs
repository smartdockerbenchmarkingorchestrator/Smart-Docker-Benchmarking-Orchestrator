using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class BenchmarkApplicationVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnvironmentVariables",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "TestPlanVariableOverrides",
                table: "BenchmarkExperiments");

            migrationBuilder.AddColumn<Guid>(
                name: "DatabaseHostId",
                table: "BenchmarkExperiments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BenchmarkExperimentVariables",
                columns: table => new
                {
                    DateTimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    ExperimentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkExperimentVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenchmarkExperimentVariables_BenchmarkExperiments_Experimen~",
                        column: x => x.ExperimentId,
                        principalTable: "BenchmarkExperiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkExperiments_DatabaseHostId",
                table: "BenchmarkExperiments",
                column: "DatabaseHostId");

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkExperimentVariables_ExperimentId",
                table: "BenchmarkExperimentVariables",
                column: "ExperimentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenchmarkExperiments_DockerHosts_DatabaseHostId",
                table: "BenchmarkExperiments",
                column: "DatabaseHostId",
                principalTable: "DockerHosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenchmarkExperiments_DockerHosts_DatabaseHostId",
                table: "BenchmarkExperiments");

            migrationBuilder.DropTable(
                name: "BenchmarkExperimentVariables");

            migrationBuilder.DropIndex(
                name: "IX_BenchmarkExperiments_DatabaseHostId",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "DatabaseHostId",
                table: "BenchmarkExperiments");

            migrationBuilder.AddColumn<string>(
                name: "EnvironmentVariables",
                table: "BenchmarkExperiments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestPlanVariableOverrides",
                table: "BenchmarkExperiments",
                nullable: true);
        }
    }
}
