using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Migrations
{
    public partial class CSVFieldChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CSVResultsFile",
                table: "BenchmarkExperiments",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 16,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CSVResultsFile",
                table: "BenchmarkExperiments",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);
        }
    }
}
