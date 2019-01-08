using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class CSVFieldApacheJmeterIgnoreField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CSVResultsFile",
                table: "BenchmarkExperiments",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "ThreadNamesToIgnore",
                table: "ApacheJmeterTestFiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CSVResultsFile",
                table: "BenchmarkExperiments");

            migrationBuilder.DropColumn(
                name: "ThreadNamesToIgnore",
                table: "ApacheJmeterTestFiles");
        }
    }
}
